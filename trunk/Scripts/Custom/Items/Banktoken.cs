/* ==============================*\
|Forged Worlds:  Banktoken.cs     |
|  By Sparkin                     |
| Ver:   1.1                      |
|                                 |
|Visit Forged Worlds On The Web   |
| http://www.forgedworlds.com     |
|                                 |
\*===============================*/

//Silver and my self was pondering adding bank access for players,
//And did not like the *Free* access for players bank accounts.
//So looking at the Pet Summoning ball We came up with this idea
//to use Powder of Translocation on a item with Limited charges and
//to use as quest items.  This is what we came up with.

//Some cleanup and modifications by Ashlar, beloved of Morrigan.

//What this is is a token item that can be given to players to access there bank boxes.
//It uses powder of Translocation to Charge for a max of 255 uses plus  the 10 - 15 charges it starts with
//Hope you find this script Useful as we do.

using System;

namespace Server.Items
{
    public class BankToken : Item, TranslocationItem
    {
        private int m_Charges;
        private int m_Recharges;
        private Mobile m_Owner;

        [CommandProperty( AccessLevel.GameMaster )]
        public int Charges
        {
            get { return m_Charges; }
            set
            {
                if ( value > this.MaxCharges )
                    m_Charges = this.MaxCharges;
                else if ( value < 0 )
                    m_Charges = 0;
                else
                    m_Charges = value;

                InvalidateProperties();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int Recharges
        {
            get { return m_Recharges; }
            set
            {
                if ( value > this.MaxRecharges )
                    m_Recharges = this.MaxRecharges;
                else if ( value < 0 )
                    m_Recharges = 0;
                else
                    m_Recharges = value;

                InvalidateProperties();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MaxCharges { get { return 20; } }

        [CommandProperty( AccessLevel.GameMaster )]
        public int MaxRecharges { get { return 100; } }

        public string TranslocationItemName { get { return "A Bank Token"; } }

        [Constructable]
        public BankToken()
            : base( 0x2AAA )
        {

            Weight = 10.0;
            Light = LightType.Circle150;
            LootType = LootType.Blessed;


            m_Charges = Utility.RandomMinMax( 10, 15 );
            m_Recharges = 0;
            m_Owner = null;
        }
        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
            list.Add( 1042971, "A bank token." );
            list.Add( 1054132, Charges.ToString() );
            list.Add( 1070722, "Recharges remaining: " + ( MaxRecharges - Recharges ).ToString() );
        }
        public override void OnDoubleClick( Mobile from )
        {
            if ( from.Criminal )
            {
                from.SendMessage( "Thou art a criminal and cannot access thy bank box." );
            }
            else if ( IsChildOf( from.Backpack ) )
            {
                if ( this.m_Owner != from )
                    this.m_Owner = from;

                if ( CanOpenBank( from, this ) )
                    DoOpenBank( from, this );
            }
            else
            {
                if ( m_Owner == null )
                    m_Owner = from;
                if ( CanOpenBank( from, this ) && from == this.m_Owner )
                    DoOpenBank( from, this );
            }
        }
        public bool CanOpenBank( Mobile from, BankToken bt )
        {
            if ( bt.Charges > 0 )
            {
                return true;
            }
            else
            {
                from.SendLocalizedMessage( 502412 ); // There are no charges left on that item.
                return false;
            }
        }
        public void DoOpenBank( Mobile from, BankToken bt )
        {
            BankBox box = from.BankBox;

            if ( box != null )
            {
                box.Open();
                bt.Charges--;
                bt.Name = "A bank token. Charges: " + Charges + " Recharges left: " + ( MaxCharges - Recharges );
            }
            else
                from.SendMessage( "Please page a gm immediately... you seem to have misplaced your bankbox....Boggle!" );

        }

        public BankToken( Serial serial )
            : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.WriteEncodedInt( ( int )0 ); // version

            writer.WriteEncodedInt( ( int )m_Charges );
            writer.WriteEncodedInt( ( int )m_Recharges );
            writer.Write( m_Owner );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadEncodedInt();

            m_Charges = reader.ReadEncodedInt();
            m_Recharges = reader.ReadEncodedInt();
            m_Owner = reader.ReadMobile();
        }
    }
}
