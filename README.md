# Reward Randomizer

![Screenshot of the application](https://i.imgur.com/55eZzzM.png)

This application is designed to shuffle / randomize certain obtainable items in:

* Fire Emblem: The Binding Blade (Japanese release)
* Fire Emblem: The Binding Blade (localization patch 1.1.3)
* Fire Emblem: The Blazing Blade (North American release)
* Fire Emblem: The Sacred Stones (North American release)

The Visual Basic application, **RewardRandomizer.VBGUI**, allows users to:

* Shuffle or randomize promotion items
* Shuffle or randomize stat boosters
* Shuffle items that are found in chests, villages, and/or desert
    * Optionally, consumable items (vulneraries, elixirs, etc.) can be left
      out of the randomization process

Items exclusive to one route in a route split will generally be left out of
the randomization process; see below for more details.

Loading a ROM image will cause the GUI to pick the appropriate game (as long
as all items are at the proper locations in ROM; otherwise, the randomizer
will not work properly). Use the "Apply" button to save an output ROM image.
Alternatively, you can pick a game without loading a ROM image, and use the
"Apply" button to create an IPS patch.

For an already-patched ROM, you can load the ROM, pick the correct game, and
use the "Validate" button to get a list of item locations that have changed.

System requirements:

* Windows 8.1+
* .NET Framework 4.7.2+

## Techical details

For each supported title, the RewardRandomizer F# library contains a list of
obtainable items and their locations in ROM. This includes items obtained by
visiting villages, opening chests, digging in the desert, or given through
story events, as well as certain other items that are stealable from enemies
or belonging to playable units (including all promotion items and stat boosters).

Items are split into four categories:

* Consumable
* Promotion
* StatBooster
* Other

Item locations (rewards) are split into five methods:

* Chest
* Village
* Starting inventory
* Story
* Desert/sand

The **RewardRandomizer** library allows software to calculate and apply one or
more shuffle / randomization operations.

Item locations (rewards) are defined by:

* Method
* Item ID (RewardRandomizer has an item name lookup table for each game)
* Unit ID (if in a unit's starting inventory, or given to them as part of a story event)
* Offsets (the locations in ROM at which the item ID appears)
* Route (the route split, if any, that the item is exclusive to)

Some items are defined as mututally exclusive; these items get treated as a
single item with multiple offsets by the randomizer as long as all properties
(other than offset) match. If other properties do not match, the item is
ignored.

Item location objects with the same item ID, but on different routes, are
combined into sets by the `Correlator` - but only if a match is found on each
of the other corresponding routes (e.g. Eirika/Ephraim, Ilia/Sacae), otherwise
the item is ignored. If an item is not route-specific, it is included in a set
of its own.

The `Randomizer` then uses this list of sets as its basis, although which
items get included when calculating the list of sets is constrained by the
input parameters:

* The input game metadata (item list, item location list, etc.)
* The mode (shuffle or randomize)
    * Shuffle: all matching items will simply be shuffled around
    * Randomize: all matching items will be replaced with a matching item with the appropriate ItemCategory
* Items: a set of items to use, defined by which ItemCategories the item is, or is not, part of
* Methods: a set of methods to include (chest, village, etc.)

The output is a sequence of single-byte writes, each to one or more locations
in ROM. To chain operations together, you must update the game metadata to
reflect the generated writes; use `GenerateMultipleRandomizations` or simply
`GenerateOperations` and they'll do this for you.

`ApplyOperations` patches a ROM image, and `CreateIPS` writes an IPS patch.
(Unlike some newer patching formats, an IPS patch lacks any check against the
original file's hash, so a patch can be created without knowledge of the
original file beyond the locations where writes need to occur.)
