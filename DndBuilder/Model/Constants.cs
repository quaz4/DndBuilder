using System;
namespace DndBuilder.Model
{
    // Constants class stores constants used throughout the program
    // They're stored here to make altering them simple, since you
    // only have to alter their value once, rather than hunting down every
    // reference in the program
    public class Constants
    {
        public static string API_URL = "http://dnd5eapi.co/api";
        public static string DB_NAME = "DndBuilder.sqlite";
        public static int PAGE_LIMIT = 10;
    }
}
