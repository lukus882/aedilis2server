using System;
using Server;
using System.Collections.Generic;

namespace Fatima.CharacterFlags
{
	public abstract class BaseCharacterFlag
	{
		private FlagValue m_Value;

		//public FlagValue Value{ get{ return m_Value; }set{ m_Value = value;} }

		public abstract string Description{ get; }
		public abstract string HashKey{ get; }

		public virtual bool CanViewFlag{ get{ return true; } }

		public virtual string ProgressDetails()
		{
			return String.Empty;
		}

		public BaseCharacterFlag()
		{
		}

		//VALID Flags begin at 1; 0 is reserved for "no flags"
		public bool HasFlag( int flag ) //Integer flag value not corresponding to the bit-version. 1, 2, 3..etc -- Adding one to compensate for 0 = 'None' in all Flags
		{
			if ( m_Value == null || flag < 0 )
				return false;

			//OLD METHOD. PRE-DYNAMICS
			//ulong bitFlag = (ulong)Math.Pow(2,flag-1); //Get the flag value. 2^0 = 1, 2^1 = 2, 2^2 = 4..etc
			//( (m_Value & bitFlag) != 0 );

			return m_Value.hasFlag( flag ); 
		}

		public void SetFlag( int flag, bool value )
		{
			if ( flag < 0 )
				return;

			/* OLD METHOD. PRE-DYNAMICS
				ulong bitFlag = (ulong)Math.Pow(2,flag-1); //Get the flag value. 2^0 = 1, 2^1 = 2, 2^2 = 4..etc -- Adding one to compensate for 0 = 'None' in all Flags
	
				if (bitFlag <= 0)
					return;

				m_Value = value ? (m_Value | bitFlag) : (m_Value & ~bitFlag);
			*/

			if (m_Value == null)
				m_Value = new FlagValue();

			m_Value.setFlag( flag, value );
		}

		public static BaseCharacterFlag Create( GenericReader reader )
		{
			Type t = Type.GetType( reader.ReadString() ); //Read the full-name Type from the string.

			BaseCharacterFlag flag = BaseCharacterFlag.Create(t);

			return flag;
		}

		public int FlagLength
		{
			get
			{
				if (m_Value == null)
					m_Value = new FlagValue();

				return m_Value.Length;
			}
		}

		public ulong getValue( int flagID )
		{
			if (m_Value == null)
				m_Value = new FlagValue();

			return m_Value.getValue( flagID );
		}

		public static BaseCharacterFlag Create(Type t)
		{
			try
			{
				object o = null;

				o = Activator.CreateInstance( t );

				if (o != null && (o is BaseCharacterFlag) )
				{
					BaseCharacterFlag bcFlag = o as BaseCharacterFlag;

					return bcFlag;
				}
			}
			catch{}

			return null;
		}

                public virtual void Serialize( GenericWriter writer ) 
                {
                        writer.Write( (int) 0 ); //version

			if ( m_Value == null )
				m_Value = new FlagValue();

			m_Value.Serialize( writer ); //serialize all the embedded peices.
                }

                public virtual void Deserialize(GenericReader reader) 
                {
                        int version = reader.ReadInt(); 

			switch ( version )
			{
				case 0:
				{
					m_Value = FlagValue.Deserialize( reader );

					break;
				}
			}
                }

		private class FlagValue
		{
			private List<ulong> m_Values; //64-bit values

			public int Length
			{
				get 
				{ 
					if ( m_Values == null )
						return 0;

					return m_Values.Count;
				}
			}

			public FlagValue()
			{
				m_Values = new List<ulong>();
				m_Values.Add( (ulong)0 );
			}
	
			public FlagValue( List<ulong> lst )
			{
				if (lst == null)
				{
					m_Values = new List<ulong>();
					m_Values.Add( (ulong)0 );
					return;
				}
	
				if ( lst.Count == 0 )
				{
					lst.Add( (ulong)0 );
				}
	
				m_Values = lst;
			}
	
			public ulong getValue( int flagID )
			{
				if ( flagID < 0 || m_Values == null )
					return 0;
	
				if ( m_Values.Count == 0 )
					return 0;
	
				int series = (int)Math.Floor( (double)(flagID / 64) ); //get the current block
	
				if ( series >= m_Values.Count )
					return 0;
	
				return m_Values[ series ]; //return the value of that block
			}
	
			private void sizeUpArray( int amt )
			{
				if ( amt <= 0 )
					return;
	
				for( int index = 0; index < amt; index++ )
				{
					m_Values.Add( (ulong)0 );
				}
			}
	
			public void setValue( int flagID, ulong newValue )
			{
				if ( flagID < 0 )
					return;
	
				if ( m_Values == null )
					m_Values = new List<ulong>();
	
				int series = (int)Math.Floor( (double)(flagID / 64) ); //get the current block
	
				if ( series >= m_Values.Count )
				{
					int diff = series - m_Values.Count;
					sizeUpArray( diff + 1 );
				}
	
				m_Values[ series ] = newValue; //set the value of that block
			}
	
			public void setFlag( int flagID, bool value )
			{
				if ( flagID < 0 )
					return;
	
				if ( m_Values == null )
					m_Values = new List<ulong>();
	
				int series = (int)Math.Floor( (double)(flagID / 64) ); //get the current block
	
				if ( series >= m_Values.Count )
				{
					int diff = series - m_Values.Count;
					sizeUpArray( diff + 1 );
				}
	
				ulong subFlagID = (ulong)Math.Pow(2, flagID - ( 64 * series ));
	
				m_Values[ series ] = value ? (m_Values[ series ] | subFlagID) : (m_Values[ series ] & ~subFlagID);
			}

			public bool hasFlag( int flagID )
			{
				if ( flagID < 0 || m_Values == null )
					return false;
	
				if ( m_Values.Count == 0 )
					return false;
	
				int series = (int)Math.Floor( (double)(flagID / 64) ); //get the current block
	
				if ( series >= m_Values.Count )
					return false;
	
				ulong subFlagID = (ulong)Math.Pow(2, flagID - ( 64 * series ));
	
				return ( (m_Values[ series ] & subFlagID) != 0 );
			}
	
			public void Serialize( GenericWriter writer )
			{
				writer.Write( (byte) 0 ); //version
	
				writer.Write( (int)m_Values.Count ); //amount of nested flags
	
				foreach( ulong val in m_Values )
				{
					writer.Write( (ulong)val );
				}
			}
	
			public static FlagValue Deserialize( GenericReader reader )
			{
				byte ver = reader.ReadByte();
	
				int flagCount = reader.ReadInt();
	
				List<ulong> lst = new List<ulong>();
				for( int index=0; index< flagCount; index++ )
				{
					lst.Add( reader.ReadULong() );
				}
	
				return new FlagValue(lst);
			}
		}
	}
}