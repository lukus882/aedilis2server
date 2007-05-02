using System;
using System.Collections.Generic;
using Server;

namespace Fatima.CharacterFlags
{
	public class CharacterFlags
	{
		public static void SerializeFlags( GenericWriter writer, Dictionary<string, BaseCharacterFlag> flags )
		{
			if (flags == null)
				flags = new Dictionary<string, BaseCharacterFlag>();

			List<string> keys = new List<string>(flags.Keys);

			writer.Write( keys.Count ); //Write the amount of keys in the collection.
			for(int i=0;i<keys.Count;i++)
			{
				string key = keys[i] as String;

				writer.Write( key ); //Write the Key

				BaseCharacterFlag bcFlag = flags[key] as BaseCharacterFlag;

				writer.Write( bcFlag.GetType().FullName ); //Write the type of flag.
				bcFlag.Serialize( writer );
			}
		}

		public static Dictionary<string, BaseCharacterFlag> DeserializeFlags( GenericReader reader )
		{
			Dictionary<string, BaseCharacterFlag> flags = new Dictionary<string, BaseCharacterFlag>();

			Console.WriteLine("Reading Flags..");
			int keyCount = reader.ReadInt(); //How many keys are in the collection?
			for(int i=0;i<keyCount;i++)
			{
				string key = reader.ReadString(); //Read the string for the key.

				//Console.WriteLine("Creating Flags..");
				BaseCharacterFlag flag = BaseCharacterFlag.Create(reader);

				if (flag != null)
				{
					flag.Deserialize( reader );
					flags.Add( key, flag );
					//Console.WriteLine("Adding Flag..");
				}
			}
			
			return flags;
		}

		public CharacterFlags()
		{
		}
	}
}