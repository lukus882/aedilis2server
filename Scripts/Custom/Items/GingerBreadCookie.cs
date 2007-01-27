using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class GingerBreadCookie : Food
	{
		private InternalTimer toothache;

		[Constructable]
		public GingerBreadCookie()
			: base(Utility.Random(0x2BE1, 2))
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 0;
		}

		public GingerBreadCookie(Serial serial)
			: base(serial)
		{
		}

		public override bool Eat(Mobile from)
		{
			if (Utility.RandomDouble() > 0.33)
			{
				// Play a random "eat" sound
				from.PlaySound(Utility.Random(0x3A, 3));

				if (from.Body.IsHuman && !from.Mounted)
					from.Animate(34, 5, 1, true, false, 0);

				if (Poison != null)
					from.ApplyPoison(Poisoner, Poison);

				if (Utility.RandomDouble() < 0.05)
					GiveToothAche(from, 0);
				else
					from.SendLocalizedMessage(1077387);
				Consume();
				return true;
			}
			from.SendLocalizedMessage(Utility.Random(1077405, 5));
			return false;
		}

		public void GiveToothAche(Mobile from, int seq)
		{
			if (toothache != null)
				toothache.Stop();
			from.SendLocalizedMessage(1077388 + seq);
			if (seq < 5)
			{
				toothache = new InternalTimer(this, from, seq, TimeSpan.FromSeconds(15));
				toothache.Start();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		private class InternalTimer : Timer
		{
			private GingerBreadCookie i_cookie;
			private int i_sequencer;
			private Mobile i_mobile;

			public InternalTimer(GingerBreadCookie cookie, Mobile m, int sequencer, TimeSpan delay)
				: base(delay)
			{
				Priority = TimerPriority.OneSecond;
				i_cookie = cookie;
				i_mobile = m;
				i_sequencer = sequencer;
			}

			protected override void OnTick()
			{
				if (i_mobile != null)
				{
					i_cookie.GiveToothAche(i_mobile, i_sequencer + 1);
				}
			}
		}
	}
}