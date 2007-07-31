

<<<<<<<<<<<<<<<<<<<< Vhaerun's Necromantic Summoning and Grave Digging >>>>>>>>>>>>>>>>>>>


This is actually two systems in one and they are interdependent. They are designed
to work together, so using one without the other would require quite a bit of
modifications.


				GRAVE DIGGING

This is based on an older grave digging script originally made by RoninGT. There are
only a few changes. I've kept the grave artifacts and "truly rare items", but simply
altered the bones to be the reagent/resource and made it just a little more difficult
to gravedig.

I was debating on making the base skill ItemID instead of Mining, but decided that
may make people a little pissed. :)

The one thing I did add to RoninGT's great system is the ability to get a Soul Stone,
which is the necessity for necromantic summoning. It's difficult to get, but with a
high enough skill in mining, you'll eventually get it.

Other little modifications include increasing the uses of the grave digger's shovel,
adding a mobile vendor to actually sell the tool, instead of the quest (just as an
option), and removing the chance you may inadvertantly "disturb the dead". While it
is a nice touch, for the system as a whole, it created a lot more difficulty than
is needed, considering it's used as a base for the necromantic summoning. The
original quest script is not included. The one I have for this system is heavily
modified because of our custom map shard and history, and uses several custom
graphics that I'd rather not make you patch to. :)

For those of you using RoninGT's grave digging system, I will be including a
seperate RAR file that includes the slightly modified grave digger's shovel that
will work with the necro summoning system. Make sure you get that script and not
the full system I've redone.


				NECRO SUMMONING

Okay, here's the real guts of the system, and I will give this warning now...

		<<<<< THIS CAN MAKE NECROMANCY EXTREMELY POWERFUL >>>>>

That being said, you need a very high level of Necromancy to really do anything with
this, and there are some serious drawbacks in certain areas.

This system allows necromancers to first craft "essences" of certain undead mobiles,
which in turn, are used (with another necromancy skill check at a higher level) to
summon the mobile. So where does the overpower part come in? The summoned mobiles have
altered numbers of control slots, depending on the power of the mobile.

		Skeleton:		0
		Bogle:			1
		Flesh Golem:		1
		Greater Skeleton:	2
		Mummy:			3
		Skeletal Dragon:	4

So one could summon as many skeletons as he or she wished, theoretically (and if the
code holds up. I've not fully tested it yet). However, these summoned mobiles are
tied to their controller just like the golem is. Damage done to the summoned mobiles
causes damage/manaloss to the controller, so summoning a ton of them may not work
as well as one might expect.

Note, however, that this damage is scaled down. It isn't a 1:1 ratio for damage that
is caused to the summoned creatures. The higher the power of the summoned creature,
the more "connection" it has with it's controller.

		Skeleton:		5:1
		Bogle:			5:1
		Flesh Golem:		5:1
		Greater Skeleton:	5:2
		Mummy:			5:3
		Skeletal Dragon:	5:4

This means that for every 5 damage a summoned skeleton takes, the controller takes
1. For every 5 damage a summoned mummy takes, the controller takes 3.

There are no distro modifications in the Necro Summoning system. If you wish to have
this system, the only thing you'd really need from the grave digging is the soul
stone. As there are several ways to put soul stones into a shard (quest, vendor,
loot.. just to name three), I will leave that up to the individual admins.

Enjoy.

Vhaerun