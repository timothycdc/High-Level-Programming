// F# record
type InnerRecordType = { X: int; Y: int }

// F# nested record
type RecordType =
    { Q: InnerRecordType
      R: InnerRecordType }

let data: RecordType =
    { Q = { X = 1; Y = 2 }
      R = { X = 3; Y = 4 } }

let writeQY (r: RecordType) (new_value: int) : RecordType =
    { r with
        Q = { r.Q with Y = new_value } }

// Q4. Define the (general) type for a lens Lens<'A,'B> which accesses a field of type 'B in an object of type 'A as a tuple (getter,setter)? Define a specific type Mylens for a lens which selects or writes field Q of RecordType as above.

// A lens is a tuple\record where the first part of which contains a getter and setter

type Lens<'A, 'B> = ('A -> 'B) * ('B -> 'A -> 'A)
type MyLens = ((RecordType -> InnerRecordType) * (InnerRecordType -> RecordType -> RecordType))


// Q5. Suppose the type of a lens which accesses a subfield of type 'B from an object of type 'A is Lens<'A,'B> = ('A -> 'B) * ('B -> 'A -> 'A).
// Write a function lensMap : (lens: Lens<'A,'B>) (f: 'B -> 'B) (a: 'A) : 'A that updates the field selected by the lens using supplied function f.
let lensMap (lens: Lens<'A, 'B>) (f: 'B -> 'B) (a: 'A) : 'A =
    // (snd lens) (f (fst lens a)) (a)  wow, my answer was way too succinct
    let getb = fst lens
    let setb = snd lens
    let b = getb a
    let b' = f b
    setb b' a


//  Q6. Using lensMap write a function mapCAndB: (lensC: Lens<'A,'C>) -> (lensB: Lens<'A,'B>) -> (fc:'C->'C) -> (fb: 'B->'B) -> (a: 'A)
// which uses fc to change C and fb to change B fields inside a data value of type A.
// you may assume that B and C fields are separate,
// so that chnaging one does not affect the other.

let mapCAndB (lensC: Lens<'A, 'C>) (lensB: Lens<'A, 'B>) (fc: 'C -> 'C) (fb: 'B -> 'B) (a: 'A) : 'A =
    // lensMap lensC fc (lensMap lensB fb a)
    a |> lensMap lensC fc |> lensMap lensB fb
