using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.ComponentModel;

namespace Project_Forest
{
    //Controls which buttons do which actions
    class ButtonController
    {
        BinaryWriter write = null;
        BinaryReader read = null;
        Keys right;
        Keys left;
        Keys jump;
        Keys melee;
        Keys sRange;
        Keys lRange;

        Keys Right
        {
            get { return right; }
            set{
                right = value;
                UpdateControlsFile();
            }
        }
        Keys Left
        {
            get { return left; }
            set
            {
                left = value;
                UpdateControlsFile();
            }
        }

        Keys Jump
        {
            get { return jump; }
            set
            {
                jump = value;
                UpdateControlsFile();
            }
        }

        Keys Melee
        {
            get { return melee; }
            set
            {
                melee = value;
                UpdateControlsFile();
            }
        }

        Keys SRange
        {
            get { return sRange; }
            set
            {
                sRange = value;
                UpdateControlsFile();
            }
        }

        Keys LRange
        {
            get { return lRange; }
            set
            {
                lRange = value;
                UpdateControlsFile();
            }
        }

        public ButtonController()
        {
            if (File.Exists("controls.data"))
            {
                try
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
                    Stream inStream = File.OpenRead("controls.data");
                    read = new BinaryReader(inStream);
                    right = (Keys)converter.ConvertFromString(read.ReadString());
                    left = (Keys)converter.ConvertFromString(read.ReadString());
                    jump = (Keys)converter.ConvertFromString(read.ReadString());
                    melee = (Keys)converter.ConvertFromString(read.ReadString());
                    sRange = (Keys)converter.ConvertFromString(read.ReadString());
                    lRange = (Keys)converter.ConvertFromString(read.ReadString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    right = Keys.Right;
                    left = Keys.Left;
                    jump = Keys.Up;
                    melee = Keys.Z;
                    sRange = Keys.X;
                    lRange = Keys.C;
                }
                finally
                {
                    if (read != null)
                    {
                        read.Close();
                    }
                }
            }
            else
            {
                right = Keys.Right;
                left = Keys.Left;
                jump = Keys.Up;
                melee = Keys.Z;
                sRange = Keys.X;
                lRange = Keys.C;
            }
        }

        public void UpdateControlsFile()
        {
            Stream outStream = File.OpenWrite("controls.data");
            write = new BinaryWriter(outStream);
            try
            {
                write.Write(right.ToString());
                write.Write(left.ToString());
                write.Write(jump.ToString());
                write.Write(melee.ToString());
                write.Write(sRange.ToString());
                write.Write(lRange.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                write.Close();
            }
        }
    }
}
