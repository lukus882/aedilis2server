//  InstrumentTuningBox.cs                                                             //
//  by Beelzebubba                                                                     //
//                                                                                     //
//  Version 1.0                                                                        //   
//                                                                                     //
//  Put this in your custom scripts folder. Add InstrumentTuningBox as an item in      //
//  loot scripts, etc. or use [add InstrumentTuningBox to add the item in the world.   //
//                                                                                     //
//  When someone double clicks the InstrumentTuningBox they will get a targeting       //
//  cursor and a message to click on the instrument they wish to tune. When they       //
//  then click on a Lute, Drums, Tambourine, or Tambourine with Tassels a song will    //
//  play and that instrument's song will be changed. This can be done repeatedly on    //
//  the same instrument, and the song will cycle between any of the 4 songs available  //
//  for that instrument. If an instrument other than those listed above, or another    //
//  item is clicked on, a message will appear stating that you can only change the     //
//  song on Lute, Drums, or Tambourines. Additionally the instrument cannot be locked  //
//  down when changing the song.                                                       //
//                                                                                     //
//             **** Flutes and Harps sounds added by Artio ****       //
/////////////////////////////////////////////////////////////////////////////////////////

using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
    #region InstrumentTuningBoxTarget
    public class InstrumentTuningBoxTarget : Target
    {
        private InstrumentTuningBox m_Box;

		public InstrumentTuningBoxTarget( InstrumentTuningBox box ) : base( 1, false, TargetFlags.None )
		{
			m_Box = box;
		}

		protected override void OnTarget( Mobile from, object target )
        {
            #region Lute Songs
            if (target is Lute)
            {
                BaseInstrument item = (BaseInstrument)target;

                if (item.Movable == true)
                {
                    switch (item.SuccessSound)
                    {
                        case 0x4C:
                            item.SuccessSound = 0x5A5;
                            from.SendMessage("You have retuned your lute to play a new song.");
                            from.PlaySound(0x5A5);
                            break;
                        case 0x5A5:
                            item.SuccessSound = 0x5B1;
                            from.SendMessage("You have retuned your lute to play a new song.");
                            from.PlaySound(0x5B1);
                            break;
                        case 0x5B1:
                            item.SuccessSound = 0x391;
                            from.SendMessage("You have retuned your lute to play a new song.");
                            from.PlaySound(0x391);
                            break;
                        case 0x391:
                            item.SuccessSound = 0x4C;
                            from.SendMessage("You have retuned your lute to play a new song.");
                            from.PlaySound(0x4C); // This is the normal UO Lute song.
                            break;

                        default:
                            item.SuccessSound = 0x4C;
                            from.SendMessage("You have retuned your lute.");
                            from.PlaySound(0x4C); // This is the normal UO Lute song.
                            break;
                    }
                }
                else
                {
                    from.SendMessage("You may not retune a lute that is locked down.");
                }

            }
            #endregion
            
            #region Drum Songs
            else if (target is Drums)
                {
                    BaseInstrument item = (BaseInstrument)target;

                    if (item.Movable == true)
                    {

                        switch (item.SuccessSound)
                        {
                            case 0x38:
                                item.SuccessSound = 0x2EA;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x2EA);
                                break;
                            case 0x2EA:
                                item.SuccessSound = 0x4D5;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x4D5);
                                break;
                            case 0x4D5:
                                item.SuccessSound = 0x2E9;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x2E9);
                                break;
                            case 0x2E9:
                                item.SuccessSound = 0x2ED;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x2ED);
                                break;
							case 0x2ED:
                                item.SuccessSound = 0x2E7;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x2E7);
                                break;
							case 0x2E7:
                                item.SuccessSound = 0x2EC;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x2EC);
                                break;
							case 0x2EC:
                                item.SuccessSound = 0x2EB;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x2EB);
                                break;
							case 0x2EB:
                                item.SuccessSound = 0x38;
                                from.SendMessage("You have retuned your drum to play a new song.");
                                from.PlaySound(0x38); // This is the normal UO Drums song.
                                break;

                            default:
                                item.SuccessSound = 0x38;
                                from.SendMessage("You have retuned your drum.");
                                from.PlaySound(0x38); // This is the normal UO Drums song.
                                break;
                        }
                    }
                    else
                    {
                        from.SendMessage("You may not retune drums that are locked down.");
                    }
                }
            #endregion

            #region Tambourine Songs
                else if (target is Tambourine)
                {
                    BaseInstrument item = (BaseInstrument)target;

                    if (item.Movable == true)
                    {

                        switch (item.SuccessSound)
                        {
                            case 0x52:
                                item.SuccessSound = 0x4B5;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x4B5);
                                break;
                            case 0x4B5:
                                item.SuccessSound = 0x4B6;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x4B6);
                                break;
                            case 0x4B6:
                                item.SuccessSound = 0x4B7;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x4B7);
                                break;
                            case 0x4B7:
                                item.SuccessSound = 0x52;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x52); // This is the normal UO Tambourine song.
                                break;

                            default:
                                item.SuccessSound = 0x52;
                                from.SendMessage("You have retuned your tambourine.");
                                from.PlaySound(0x52); // This is the normal UO Tambourine song.
                                break;
                        }
                    }
                    else
                    {
                        from.SendMessage("You may not retune a tambourine that is locked down.");
                    }
                }
                #endregion

            #region Tambourine with Tassel Songs
                else if (target is TambourineTassel)
                {
                    BaseInstrument item = (BaseInstrument)target;

                    if (item.Movable == true)
                    {

                        switch (item.SuccessSound)
                        {
                            case 0x52:
                                item.SuccessSound = 0x4B5;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x4B5);
                                break;
                            case 0x4B5:
                                item.SuccessSound = 0x4B6;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x4B6);
                                break;
                            case 0x4B6:
                                item.SuccessSound = 0x4B7;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x4B7);
                                break;
                            case 0x4B7:
                                item.SuccessSound = 0x52;
                                from.SendMessage("You have retuned your tambourine to play a new song.");
                                from.PlaySound(0x52); // This is the normal UO Tambourine song.
                                break;

                            default:
                                item.SuccessSound = 0x52;
                                from.SendMessage("You rehave tuned your tambourine.");
                                from.PlaySound(0x52); // This is the normal UO Tambourine song.
                                break;
                        }
                    }
                    else
                    {
                        from.SendMessage("You may not retune a tambourine that is locked down.");
                    }  
                }
                
                #endregion

//flutes and harps added by Artio

			#region BambooFlute Songs
                else if (target is BambooFlute)
                {
                    BaseInstrument item = (BaseInstrument)target;

                    if (item.Movable == true)
                    {

                        switch (item.SuccessSound)
                        {
                            case 0x504:
                                item.SuccessSound = 0x58A;
                                from.SendMessage("You have retuned your flute to play a new song.");
                                from.PlaySound(0x58A);
                                break;
                            case 0x58A:
                                item.SuccessSound = 0x58B;
                                from.SendMessage("You have retuned your flute to play a new song.");
                                from.PlaySound(0x58B);
                                break;
                            case 0x58B:
                                item.SuccessSound = 0x5B7;
                                from.SendMessage("You have retuned your flute to play a new song.");
                                from.PlaySound(0x5B7);
                                break;
                            case 0x5B7:
                                item.SuccessSound = 0x504;
                                from.SendMessage("You have retuned your flute to play a new song.");
                                from.PlaySound(0x504); // This is the normal UO BambooFlute song.
                                break;

                            default:
                                item.SuccessSound = 0x04;
                                from.SendMessage("You have retuned your flute.");
                                from.PlaySound(0x504); // This is the normal UO BambooFlute song.
                                break;
                        }
                    }
                    else
                    {
                        from.SendMessage("You may not retune a flute that is locked down.");
                    }
                }
                #endregion

			#region Harp Songs
                else if (target is Harp)
                {
                    BaseInstrument item = (BaseInstrument)target;

                    if (item.Movable == true)
                    {

                        switch (item.SuccessSound)
                        {
                            case 0x43:
                                item.SuccessSound = 0x393;
                                from.SendMessage("You have retuned your harp to play a new song.");
                                from.PlaySound(0x393);
                                break;
                            case 0x393:
                                item.SuccessSound = 0x392;
                                from.SendMessage("You have retuned your harp to play a new song.");
                                from.PlaySound(0x392);
                                break;
                             case 0x392:
                                item.SuccessSound = 0x43;
                                from.SendMessage("You have retuned your harp to play a new song.");
                                from.PlaySound(0x43); // This is the normal UO Harp song.
                                break;

                            default:
                                item.SuccessSound = 0x43;
                                from.SendMessage("You have retuned your harp.");
                                from.PlaySound(0x43); // This is the normal UO Harp song.
                                break;
                        }
                    }
                    else
                    {
                        from.SendMessage("You may not retune a harp that is locked down.");
                    }
                }
                #endregion

			#region LapHarp Songs
                else if (target is LapHarp)
                {
                    BaseInstrument item = (BaseInstrument)target;

                    if (item.Movable == true)
                    {

                        switch (item.SuccessSound)
                        {
                            case 0x43:
                                item.SuccessSound = 0x45;
                                from.SendMessage("You have retuned your lap harp to play a new song.");
                                from.PlaySound(0x45);
                                break;
                            case 0x45:
                                item.SuccessSound = 0x392;
                                from.SendMessage("You have retuned your lap harp to play a new song.");
                                from.PlaySound(0x392);
                                break;
							case 0x392:
                                item.SuccessSound = 0x393;
                                from.SendMessage("You have retuned your lap harp to play a new song.");
                                from.PlaySound(0x393);
                                break;
                             case 0x393:
                                item.SuccessSound = 0x43;
                                from.SendMessage("You have retuned your lap harp to play a new song.");
                                from.PlaySound(0x43); // This is the normal UO Harp song.
                                break;

                            default:
                                item.SuccessSound = 0x43;
                                from.SendMessage("You have retuned your lap harp.");
                                from.PlaySound(0x43); // This is the normal UO Harp song.
                                break;
                        }
                    }
                    else
                    {
                        from.SendMessage("You may not retune a lap harp that is locked down.");
                    }
                }
                #endregion
        }
    }
    #endregion

    #region InstrumentTuningBox
        public class InstrumentTuningBox : Item
	{
		public override string DefaultName
		{
            get { return "An Instrument Tuning Box"; }
		}

		[Constructable]
		public InstrumentTuningBox() : base( 0x2AF9 )
		{
			Weight = 10.0;
		}

		public InstrumentTuningBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from )
		{
            from.SendMessage("What instrument would you like to retune?");
			from.Target = new InstrumentTuningBoxTarget( this );
		}
    }
    #endregion
}