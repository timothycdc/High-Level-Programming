# Team-Based Learning Material

## TBL1
[Transforms Notes](https://github.com/tomcl/fsharp-transforms/blob/main/README.md)
[ISSIE Coding Guidelines](https://github.com/tomcl/issie/wiki/1---Coding-guidelines-for-ISSIE)
[Slides (requires login)](https://intranet.ee.ic.ac.uk/t.clarke/hlp/lectures/tbl24-1.pdf)

### TBL1 Feedback
Whole Cohort Feedback
A.
```fsharp
/// Return absolute segment list from a wire.
/// NB - it is often more efficient to use various fold functions (foldOverSegs etc)
let getAbsSegments (wire: Wire) : ASegment list =
    let convertToAbs ((start,dir): XYPos*Orientation) (seg: Segment) =
        {Start=start; End = addLengthToPos start dir seg.Length; Segment = seg}
    (((wire.StartPos,wire.InitialOrientation),[]), wire.Segments)
    ||> List.fold (fun (posDir, aSegL) seg -> 
            let nextASeg = convertToAbs posDir seg
            let posDir' = nextASeg.End, switchOrientation (snd posDir)
            posDir', (nextASeg :: aSegL))
    |> snd
    |> List.rev


/// Return absolute segment list from a wire.
/// NB - it is often more efficient to use various fold functions (foldOverSegs etc)
let getNonZeroAbsSegments (wire: Wire) : ASegment list =
    let convertToAbs ((start,dir): XYPos*Orientation) (seg: Segment) =
        {Start=start; End = addLengthToPos start dir seg.Length; Segment = seg}
    (((wire.StartPos,wire.InitialOrientation),[]), wire.Segments)
    ||> List.fold (fun (posDir, aSegL) seg -> 
            let nextASeg = convertToAbs posDir seg
            let posDir' = nextASeg.End, switchOrientation (snd posDir)
            if not <| seg.IsZero then
                posDir', (nextASeg :: aSegL)
            else
                posDir', aSegL)                
    |> snd
|> List.rev
```

Here a common getAbsSegments (excludeZeroSegments: bool) (wire:Wire) would be a great solution. If inline it would have zero overhead. Both these functions can be defined using it. Strong reason to do this: great example. Arguably it could have a “includeSegment” predicate and be more general – but that would be less efficient and for this function (unusually) speed maybe matters.

B. Some had this is positive example of pipeline, some said it was too long. It is positive. The anon function parameters make clear the data. The operations are clear. Breaking it up would make it less clear.
```fsharp
// Get pairs of unique symbols that are connected to each other.
/// HLP23: AUTHOR dgs119
let getConnSyms (wModel: BusWireT.Model) =
    wModel.Wires
    |> Map.values
    |> Seq.toList
    |> List.map (fun wire -> (getSourceSymbol wModel wire, getTargetSymbol wModel wire))
    |> List.filter (fun (symA, symB) -> symA.Id <> symB.Id)
|> List.distinctBy (fun (symA, symB) -> Set.ofList [ symA; symB ])
```

C.
```fsharp
/// Returns length of wire
/// HLP23: AUTHOR Jian Fu Eng (jfe20)
let getWireLength (wire: Wire) : float =
(0., wire.Segments) ||> List.fold (fun acc seg -> acc + (abs seg.Length))
```
This pipeline should be arranged vertically – as is the absolute convention for Issie folds. The anonymous function at the end of a pipeline is also the convention for Issie folds. Anonymous functions come into their own like this when longer – in which case they have newline after ->
D.
```fsharp
/// Checks if a wire is part of a net.
/// If yes, return the netlist. Otherwise, return None
/// HLP23: AUTHOR Jian Fu Eng (jfe20)
let isWireInNet (model: Model) (wire: Wire) : (OutputPortId * (ConnectionId * Wire) list) option =
    let nets = partitionWiresIntoNets model

    nets
|> List.tryFind (fun (outputPortID, netlist) -> wire.OutputPort = outputPortID && netlist |> List.exists (fun (connID, w) -> connID <> wire.WId))
```

The pipeline anon functions here are confusing because the line is not folded vertically. The 2nd pipeline is inside an expression. In this case it is meaningful so it is best to use a let definition. Lining up the halves of the && vertically, folding at the 1st -> all  helps as in D2. The 2nd half (pipeline netlist |> … ) could become a subfunction (also used in D2).

The let definition used – nets – is hardly worth anything and could be pipelined. D2 shows a good solution where the both anonymous functions stay anonymous but a meaningful let defn is added.

D2
```fsharp
/// Checks if a wire is part of a net.
/// If yes, return the netlist. Otherwise, return None
/// HLP23: AUTHOR Jian Fu Eng (jfe20)
let isWireInNet (model: Model) (wire: Wire) : (OutputPortId * (ConnectionId * Wire) list) option =

let wireNotInNetlist netlist wire = 
netlist 
|> List.exists (fun (connID, _) -> connID <> wire.WId))

    	partitionWiresIntoNets model
|> List.tryFind (fun (outputPortID, netlist) -> 
wire.OutputPort = outputPortID && 
	wireNotInList netlist wire)
```

E.
```fsharp
/// HLP23: AUTHOR dgs119
let inline getPortOrientationFrmPortIdStr (model: SymbolT.Model) (portIdStr: string) : Edge = 
```
the function name is too long. Question is why? And how to shorten? (1) port is duplicated – 1st port not needed. (2) str indicates type – string rather than D.U. wrapped string. Not needed since clear from (and forced by, so can’t go wrong) type. (3) Frm can be removed by reordering.
We get:
```fsharp
let inline getPortIdOrientation (model: SymbolT.Model) (portId: string) : Edge = 
```

F.
This is a great use of input wrapping (initPos, initOrientation) – even though it is very simple. The code is complex enough that the simplification coming from the wrapping is very helpful. In addition the names help comprehension.
```fsharp
let inline foldOverNonZeroSegs folder state wire =
    let initPos = wire.StartPos
    let initOrientation = wire.InitialOrientation
    ((state, initPos, initOrientation), wire.Segments)
    ||> List.fold (fun (currState, currPos, currOrientation) seg -> 
        let nextOrientation = switchOrientation currOrientation
        if seg.IsZero then 
            (currState, currPos, nextOrientation)
        else
            let nextPos = addLengthToPos currPos currOrientation seg.Length
            let nextState = folder currPos nextPos currState seg
            (nextState, nextPos, nextOrientation))
|> (fun (state, _, _) -> state)
```
G.
```fsharp
let inline foldOverSegs folder state wire =
    let initPos = wire.StartPos
    let initOrientation = wire.InitialOrientation
    ((state, initPos, initOrientation), wire.Segments)
    ||> List.fold (fun (currState, currPos, currOrientation) seg -> 
        let nextPos = addLengthToPos currPos currOrientation seg.Length
        let nextOrientation = switchOrientation currOrientation
        let nextState = folder currPos nextPos currState seg
        (nextState, nextPos, nextOrientation))
    |> (fun (state, _, _) -> state)


let inline foldOverNonZeroSegs folder state wire =
    let initPos = wire.StartPos
    let initOrientation = wire.InitialOrientation
    ((state, initPos, initOrientation), wire.Segments)
    ||> List.fold (fun (currState, currPos, currOrientation) seg -> 
        let nextOrientation = switchOrientation currOrientation
        if seg.IsZero then 
            (currState, currPos, nextOrientation)
        else
            let nextPos = addLengthToPos currPos currOrientation seg.Length
            let nextState = folder currPos nextPos currState seg
            (nextState, nextPos, nextOrientation))
    |> (fun (state, _, _) -> state)
```


- Input wrapping is very helpful here because it reduces complexity of complex expressions and also the names help to indicate what the value is in context.
- The pipeline at end is appropriate and clearer than anything else
- The let definitions in the middle again make things more readable.
- AddLengtToPos and switchOrientation are good examples of functional abstraction







## TBL2
[Slides (requires login)](https://intranet.ee.ic.ac.uk/t.clarke/hlp/lectures/hlp24-tick3-starter.pdf)
[Project Specification 2024 (requires login)](https://intranet.ee.ic.ac.uk/t.clarke/hlp/images/project2024%20specification.pdf)