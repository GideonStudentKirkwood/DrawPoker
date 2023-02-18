using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /*
        Basic O-O Concepts / Foundational Ideas
        Classes are templates for making objects
        Objects are program components that include
            data, and the methods for using that data

        Encapsulation:  data-hiding, data privacy is wrapping
                        up variables together into classes
                        and keeping outside code from affecting
                        them directly. This requires methods
                        to be included that operate on the data.
                        "Tell, don't ask.", "private fields"

        Abstraction:    creating classes from categories, usually
                        to use as base classes (parent classes)
                        for example, the abstract base class
                        of everything in C#, C++, or Java is
                        the system.object class.
        
        Inheritance     we can create more specific child classes
                        from parent classes, and have them inherit
                        the properties and methods we defined in
                        the parent
        
        Polymorphism    the ability to use a child object as if
                        it were its parent class (as long as all
                        we need are the parent class properties
                        and methods) and the ability to cast it
                        back to its true child class when we need
                        to use those properties

        Composition     the ability to build more complex classes
                        by including private fields, that are
                        instances of simpler classes.
    */
    public enum Suit
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }

    public enum FaceValue
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public enum HandValue
    {
        Nothing = 0,
        Pair = 1,
        Two_Pair = 3,
        Three_of_a_Kind = 10,
        Straight = 30,
        Flush = 50,
        Full_House = 75,
        Four_of_a_Kind = 100,
        Straight_Flush = 250,
        Royal_Flush = 500
    }
}
