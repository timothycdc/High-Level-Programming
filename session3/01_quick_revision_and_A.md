## Quick Revision
1. Tuples and other patterns often need to be enclosed in brackets in F#. In which contexts is this necessary?
   1. In functions `f (a,b) = `, anonymous functions `fun (a,b) -> `, but not required for match expressions `match x in a,b -> `
   2. 


2. What is the F# type of a function that takes as parameter a Curried function of two int arguments returning int, and which returns a function that transforms strings into modified strings? Give the type without unnecessary brackets.
   1. Parameter is `int -> int -> int`
   2. Return type is `string -> string`
   3. Function type is `(int -> int -> int )-> string -> string`
   4. 


3. An (immutable) F# function is written using array datatypes throughout, with `Array` collection functions. Can it be rewritten without other change using lists? Maps? What about the other way round?
   1. It can be rewritten with lists (except element update), but not with maps.
   2. Professor's long answer: Arrays can always be rewritten as`int ->T_Map`. However the corresponding functions are different, and changes are needed in code. The other way round List functions can normally be transformed into Array functions with one exception. there is no nice array equivalent to`List.Head`,`List.Tail`. In addition there is no array equivalent of the `h :: t` pattern that splits a list into head and tail. Usually functions using lists with list collection functions do not use these operations, and can be rewritten using arrays.


4. What is the difference between `map` and `Map` in F#?
   1. `map` is a function from List.map
   2. `Map` is a dictionary module


5. Why does `let print = printf "%A"` not work as a definition of a general print function when `let print x = printfn "%A x `does work?
   1. The former generates a compile-time value restriction type inference error. A wildcard polymorphic type for x cannot be used as a part of another type without being explicit. The reasons are complex, one is that the compiler cannot know whether the type of x will be used in a generic context or not. 
   2. A generic context is one where the type of x is not known at compile time, but is determined at run time. This is the case for example when x is a function parameter, or a list element. The latter is not a compile-time value restriction type inference error, because the type of x is known at compile time.


6. what is the difference between a function argument and a function parameter?
   1. Professor:Technically, a (formal) parameter is the local variable bound to the (actual) argument value in a function call.
   2. ```fsharp 
        let f a b = a + b
        f (3+10) 4
        ```
      Here the first parameter of `f` is `a`, and the first argument of `f` is `(3+4)` or `7`.
   3. Simplified: parameter is the variable name passed into the function, argument is the (parameter's) value passed to the function.

## Part A
- An exception can be raised that causes the program execution to stop and the function stack to unwind until either the outermost level is reached, and the program stops with an error, or code is found that can catch the exception.
- Exceptions are not functional. Use `try with` instead
- Use `failwithf` to raise an exception with a formatted message. It has type `'T` and will type match everything
- So: learn about exceptions in F# to use them in pathological cases or for debugging. Aim to write normal code as far as possible such that exceptions are designed out and cannot happen anywhere
- Better to use `Result` or `Option` types to handle errors in a functional way, this will scale well to larger programs
- 