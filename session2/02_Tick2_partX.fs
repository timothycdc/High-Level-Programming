// Q7 Write a function combineLens: Lens<'A,'B> -> Lens<'B,'C> -> Lens<A,'C>

module Tick2X =

    // open System

    type Lens<'A, 'B> = ('A -> 'B) * ('B -> 'A -> 'A)

    let combineLens (l1: Lens<'A, 'B>) (l2: Lens<'B, 'C>) : Lens<'A, 'C> =
        let (getb_from_a, setb_in_a) = l1
        let (getc_from_b, setc_in_b) = l2

        let getc_from_a (a: 'A) = getc_from_b (getb_from_a a)

        let setc_from_a (c: 'C) (a: 'A) =
            setb_in_a (setc_in_b c (getb_from_a a)) a

        (getc_from_a, setc_from_a)
