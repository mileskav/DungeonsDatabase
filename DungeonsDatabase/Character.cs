using System;
using System.Collections.Generic;

namespace DungeonsDatabase
{
    class Character
    {   
        #region ENUMS
        public enum Race
        {
            DRAGONBORN,
            DWARF,
            ELF,
            GENASI,
            GNOME,
            FIRBOLG,
            ORC,
            HUMAN,
            TIEFLING
        }
        public enum Class
        {
            BARBARIAN,
            BARD,
            CLERIC,
            DRUID,
            FIGHTER,
            MONK,
            PALADIN,
            RANGER,
            ROGUE,
            SORCERER,
            WARLOCK,
            WIZARD
        }
        public enum Status
        {
            HEALTHY,
            POISONED,
            BLINDED,
            CHARMED,
            UNCONSCIOUS
        }

        #endregion

        #region FIELDS

        private string _name;
        private int _level;
        private int _exp;
        private Race _race;
        private Class _class;
        private int _hp;
        private Status _status;
        private int _strength;
        private int _dexterity;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _charisma;
        private List<string> _equipment;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
        public Race CharRace
        {
            get { return _race; }
            set { _race = value; }
        }
        public Class CharClass
        {
            get { return _class; }
            set { _class = value; }
        }
        public int Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        public Status CharStatus
        {
            get { return _status; }
            set { _status = value; }
        }
        public int Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }
        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }
        public int Constitution
        {
            get { return _constitution; }
            set { _constitution = value; }
        }
        public int Intelligence
        {
            get { return _intelligence; }
            set { _intelligence = value; }
        }
        public int Wisdom
        {
            get { return _wisdom; }
            set { _wisdom = value; }
        }
        public int Charisma
        {
            get { return _charisma; }
            set { _charisma = value; }
        }
        public List<string> Equipment
        {
            get { return _equipment; }
            set { _equipment = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Character()
        {

        }

        #endregion

    }
}
