using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    class InputClass
    {
        private const char split = ',';
        private HashSet<Key> keys;
        private int pressed = 0;

        private InputClass(HashSet<Key> keys)
        {
            this.keys = keys;
        }

        public InputClass(params Key[] keys)
        {
            this.keys = new HashSet<Key>(keys);
        }

        public void Update()
        {
            bool areAllPressed = true;
            foreach (Key key in keys)
            {
                if (!Keyboard.current[key].IsActuated())
                {
                    areAllPressed = false;
                    break;
                }
            }

            if (areAllPressed)
            {
                pressed++;
            }
            else
            {
                pressed = 0;
            }
        }

        public static InputClass fromString(string keysString)
        {
            HashSet<Key> keys = new HashSet<Key>();
            foreach (string keyString in keysString.Split(split))
            {
                keys.Add((Key)Enum.Parse(Key.A.GetType(), keyString));
            }
            return new InputClass(keys);
        }

        public override string ToString()
        {
            string value = "";
            foreach (Key key in keys)
            {
                value += Enum.GetName(key.GetType(), key);
                value += split;
            }
            return value.Substring(0, value.Length - 1);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InputClass);
        }

        public bool Equals(InputClass other)
        {
            return other != null && other.keys.Equals(keys);
        }

        public override int GetHashCode()
        {
            return keys.GetHashCode();
        }

        public bool isPressedThisFrame()
        {
            return pressed == 1;
        }

        public bool isPressed()
        {
            return pressed > 0;
        }

        public int frameCountPressed()
        {
            return pressed;
        }
    }

    class MultiInputClass
    {
        private const char split = '|';
        HashSet<InputClass> keys;
        private int pressed = 0;

        private MultiInputClass(HashSet<InputClass> keys)
        {
            this.keys = keys;
        }

        public MultiInputClass(params InputClass[] keys)
        {
            this.keys = new HashSet<InputClass>(keys);
        }

        public void Update()
        {
            bool isPressed = false;
            foreach (InputClass key in keys)
            {
                key.Update();
                if (key.isPressed())
                {
                    isPressed = true;
                }
            }
            if (isPressed)
            {
                pressed++;
            }
            else
            {
                pressed = 0;
            }
        }

        public static MultiInputClass fromString(string keysString)
        {
            HashSet<InputClass> keys = new HashSet<InputClass>();
            foreach (string keyString in keysString.Split(split))
            {
                keys.Add(InputClass.fromString(keyString));
            }
            return new MultiInputClass(keys);
        }

        public override string ToString()
        {
            string value = "";
            foreach (InputClass key in keys)
            {
                value += key.ToString();
                value += split;
            }
            return value.Substring(0, value.Length - 1);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MultiInputClass);
        }

        public bool Equals(MultiInputClass other)
        {
            return other != null && other.keys.Equals(keys);
        }

        public override int GetHashCode()
        {
            return keys.GetHashCode();
        }

        public bool isPressedThisFrame()
        {
            return pressed == 1;
        }

        public bool isPressed()
        {
            return pressed > 0;
        }

        public int frameCountPressed()
        {
            return pressed;
        }
    }
}
