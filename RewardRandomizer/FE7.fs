namespace RewardRandomizer

open Item
open Reward

module FE7 =
    let Items = [
        item 0x0 "NONE" Other
        item 0x1 "IRON_SWORD" Other
        item 0x2 "SLIM_SWORD" Other
        item 0x3 "STEEL_SWORD" Other
        item 0x4 "SILVER_SWORD" Other
        item 0x5 "IRON_BLADE" Other
        item 0x6 "STEEL_BLADE" Other
        item 0x7 "SILVER_BLADE" Other
        item 0x8 "POISON_SWORD" Other
        item 0x9 "RAPIER" Other
        item 0xa "MANI_KATTI" Other
        item 0xb "BRAVE_SWORD" Other
        item 0xc "WO_DAO" Other
        item 0xd "KILLING_EDGE" Other
        item 0xe "ARMORSLAYER" Other
        item 0xf "WYRMSLAYER" Other
        item 0x10 "LIGHT_BRAND" Other
        item 0x11 "RUNE_SWORD" Other
        item 0x12 "LANCEREAVER" Other
        item 0x13 "LONGSWORD" Other
        item 0x80 "EMBLEM_SWORD" Other
        item 0x84 "DURANDAL" Other
        item 0x8c "SOL_KATTI" Other
        item 0x90 "REGAL_BLADE" Other
        item 0x99 "WIND_SWORD" Other
        item 0x14 "IRON_LANCE" Other
        item 0x15 "SLIM_LANCE" Other
        item 0x16 "STEEL_LANCE" Other
        item 0x17 "SILVER_LANCE" Other
        item 0x18 "POISON_LANCE" Other
        item 0x19 "BRAVE_LANCE" Other
        item 0x1a "KILLER_LANCE" Other
        item 0x1b "HORSESLAYER" Other
        item 0x1c "JAVELIN" Other
        item 0x1d "SPEAR" Other
        item 0x1e "AXEREAVER" Other
        item 0x81 "EMBLEM_LANCE" Other
        item 0x91 "REX_HASTA" Other
        item 0x94 "HEAVY_SPEAR" Other
        item 0x95 "SHORT_SPEAR" Other
        item 0x1f "IRON_AXE" Other
        item 0x20 "STEEL_AXE" Other
        item 0x21 "SILVER_AXE" Other
        item 0x22 "POISON_AXE" Other
        item 0x23 "BRAVE_AXE" Other
        item 0x24 "KILLER_AXE" Other
        item 0x25 "HALBERD" Other
        item 0x26 "HAMMER" Other
        item 0x27 "DEVIL_AXE" Other
        item 0x28 "HAND_AXE" Other
        item 0x29 "TOMAHAWK" Other
        item 0x2a "SWORDREAVER" Other
        item 0x2b "SWORDSLAYER" Other
        item 0x59 "DRAGON_AXE" Other
        item 0x82 "EMBLEM_AXE" Other
        item 0x85 "ARMADS" Other
        item 0x8d "WOLF_BEIL" Other
        item 0x92 "BASILIKOS" Other
        item 0x2c "IRON_BOW" Other
        item 0x2d "STEEL_BOW" Other
        item 0x2e "SILVER_BOW" Other
        item 0x2f "POISON_BOW" Other
        item 0x30 "KILLER_BOW" Other
        item 0x31 "BRAVE_BOW" Other
        item 0x32 "SHORT_BOW" Other
        item 0x33 "LONGBOW" Other
        item 0x83 "EMBLEM_BOW" Other
        item 0x93 "RIENFLECHE" Other
        item 0x37 "FIRE" Other
        item 0x38 "THUNDER" Other
        item 0x39 "ELFIRE" Other
        item 0x3a "BOLTING" Other
        item 0x3b "FIMBULVETR" Other
        item 0x3c "FORBLAZE" Other
        item 0x3d "EXCALIBUR" Other
        item 0x3e "LIGHTNING" Other
        item 0x3f "SHINE" Other
        item 0x40 "DIVINE" Other
        item 0x41 "PURGE" Other
        item 0x42 "AURA" Other
        item 0x43 "LUCE" Other
        item 0x86 "AUREOLA" Other
        item 0x44 "FLUX" Other
        item 0x45 "LUNA" Other
        item 0x46 "NOSFERATU" Other
        item 0x47 "ECLIPSE" Other
        item 0x48 "FENRIR" Other
        item 0x49 "GESPENST" Other
        item 0x4a "HEAL" Other
        item 0x4b "MEND" Other
        item 0x4c "RECOVER" Other
        item 0x4d "PHYSIC" Other
        item 0x4e "FORTIFY" Other
        item 0x4f "RESTORE" Other
        item 0x50 "SILENCE" Other
        item 0x51 "SLEEP" Other
        item 0x52 "BERSERK" Other
        item 0x53 "WARP" Other
        item 0x54 "RESCUE" Other
        item 0x55 "TORCH_STAFF" Other
        item 0x56 "HAMMERNE" Other
        item 0x57 "UNLOCK" Other
        item 0x58 "BARRIER" Other
        item 0x5a "ANGELIC_ROBE" StatBooster
        item 0x5b "ENERGY_RING" StatBooster
        item 0x5c "SECRET_BOOK" StatBooster
        item 0x5d "SPEEDWINGS" StatBooster
        item 0x5e "GODDESS_ICON" StatBooster
        item 0x5f "DRAGONSHIELD" StatBooster
        item 0x60 "TALISMAN" StatBooster
        item 0x61 "BOOTS" StatBooster
        item 0x62 "BODY_RING" StatBooster
        item 0x63 "HERO_CREST" Promotion
        item 0x64 "KNIGHT_CREST" Promotion
        item 0x65 "ORION_BOLT" Promotion
        item 0x66 "ELYSIAN_WHIP" Promotion
        item 0x67 "GUIDING_RING" Promotion
        item 0x87 "EARTH_SEAL" Promotion
        item 0x89 "HEAVEN_SEAL" Promotion
        item 0x8a "EMBLEM_SEAL" StatBooster
        item 0x8b "FELL_CONTRACT" Promotion
        item 0x96 "OCEAN_SEAL" Promotion
        item 0x88 "AFA_DROPS" StatBooster
        item 0x68 "CHEST_KEY" Other
        item 0x78 "CHEST_KEY_5" Other
        item 0x69 "DOOR_KEY" Other
        item 0x6a "LOCKPICK" Other
        item 0x6b "VULNERARY" Other
        item 0x6c "ELIXIR" Other
        item 0x6d "PURE_WATER" Other
        item 0x6e "ANTITOXIN" Other
        item 0x6f "TORCH" Other
        item 0x79 "MINE" Other
        item 0x7a "LIGHT_RUNE" Other
        item 0x70 "DELPHI_SHIELD" Other
        item 0x71 "MEMBER_CARD" Other
        item 0x7b "IRON_RUNE" Other
        item 0x72 "SILVER_CARD" Other
        item 0x73 "WHITE_GEM" Other
        item 0x74 "BLUE_GEM" Other
        item 0x75 "RED_GEM" Other
        item 0x77 "UBER_SPEAR" Other
        item 0x8e "ERESHKIGAL" Other
        item 0x8f "FLAMETONGUE" Other
        item 0x7c "FILLA_MIGHT" Other
        item 0x7d "NINI_GRACE" Other
        item 0x7e "THOR_IRE" Other
        item 0x7f "SET_LITANY" Other
        item 0x97 "GOLD_3000" Other
        item 0x98 "GOLD_5000" Other
    ]

    let CHES offset item = reward Chest (offset + 4) item 0
    let ITGV offset item = reward Village (offset + 4) item 0
    let Unit offset item unit pos = reward StartingInventory (offset + 8 + pos) item unit
    let Chap offset item = reward Story (offset + 12) item 0
    let ChTo offset item unit = reward Story (offset + 16) item unit
    let Sand offset item = reward Sand (offset + 4) item 0

    let US = [
        // Fire Emblem (USA, Australia)

        // Chapter 11 (Eliwood): Taking Leave
        yield! List.map (route Eliwood) [
            ITGV 0xCAE43C 0x5F // DRAGONSHIELD
            Unit 0xCC7C5C 0x5B 0x08 1 // ENERGY_RING
        ]

        // Chapter 11 (Hector): Another Journey
        yield! List.map (route Hector) [
            CHES 0xCA19CC 0x75 // RED_GEM
        ]

        // Chapter 12: Birds of a Feather
        ITGV 0xCAF13C 0x5C // SECRET_BOOK

        // Chapter 13: In Search of Truth
        ITGV 0xCAF7EC 0x79 // MINE
        ITGV 0xCAF86C 0x6F // TORCH

        // Chapter 13x: The Peddler Merlinus

        // Chapter 14: False Friends
        Unit 0xCC9934 0x5E 0x13 1 // GODDESS_ICON
        ITGV 0xCB0348 0x05 // IRON_BLADE

        // Chapter 15: Talons Alight
        yield! List.map (route Hector) [
            Unit 0xCC9E24 0x5F 0x46 2 |> difficulty HectorHard // DRAGONSHIELD
            CHES 0xCA22CC 0x21 // SILVER_AXE
            CHES 0xCA22D8 0x4B // MEND
        ]

        // Chapter 16: Noble Lady of Caelin
        ITGV 0xCB0DC0 0x75 // RED_GEM
        ITGV 0xCB0E50 0x94 // HEAVY_SPEAR

        // Chapter 17: Whereabouts Unknown
        CHES 0xCA287C 0x64 // KNIGHT_CREST
        CHES 0xCA2888 0x04 // SILVER_SWORD
        CHES 0xCA2894 0x57 // UNLOCK
        CHES 0xCA28A0 0x63 // HERO_CREST
        Chap 0xCB1950 0x75 // RED_GEM
        Chap 0xCB1950 0x75 |> tag "Caelin soliders" // RED_GEM
        Chap 0xCB197C 0x7A |> tag "Caelin soliders" // LIGHT_RUNE
        Chap 0xCB19A8 0x79 |> tag "Caelin soliders" // MINE

        // Chapter 17x: The Port of Badon
        ITGV 0xCB1F10 0x12 // LANCEREAVER
        ITGV 0xCB1F50 0x51 // SLEEP
        ITGV 0xCB1F90 0x32 // SHORT_BOW
        ITGV 0xCB200C 0x27 // DEVIL_AXE

        // Chapter 18: Pirate Ship
        Unit 0xCCC32C 0x67 0x6E 1 |> difficulty Normal // GUIDING_RING
        Unit 0xCCC50C 0x67 0x4A 1 |> difficulty Hard // GUIDING_RING
        Unit 0xCCC45C 0x67 0x6E 1 |> difficulty HectorNormal // GUIDING_RING
        Unit 0xCCC65C 0x67 0x4A 1 |> difficulty HectorHard // GUIDING_RING
        Unit 0xCCC6CC 0x66 0xF3 1 |> difficulty HectorHard // ELYSIAN_WHIP

        Unit 0xCCC31C 0x5D 0x4A 1 |> difficulty Normal // SPEEDWINGS
        Unit 0xCCC44C 0x5D 0x4A 1 |> difficulty Hard // SPEEDWINGS

        // Chapter 19: The Dread Isle
        Unit 0xCCD054 0x65 0x4B 3 |> difficulty Normal // ORION_BOLT
        Unit 0xCCD314 0x65 0x4B 3 |> difficulty Hard // ORION_BOLT
        Unit 0xCCD1B4 0x65 0x4B 3 |> difficulty HectorNormal // ORION_BOLT
        Unit 0xCCD4F4 0x65 0x4B 3 |> difficulty HectorHard // ORION_BOLT

        // Chapter 19x: Imprisoner of Magic
        Unit 0xCCDF50 0x66 0x6D 1 |> difficulty Hard // ELYSIAN_WHIP
        ITGV 0xCB348C 0x5E // GODDESS_ICON

        // Chapter 19xx: A Glimpse in Time
        yield! List.map (route Hector) [
            CHES 0xCA2F50 0x5F // DRAGONSHIELD
            CHES 0xCA2F5C 0x60 // TALISMAN
            CHES 0xCA2F68 0x47 // ECLIPSE
        ]

        // Chapter 20: Dragon's Gate
        CHES 0xCA3460 0x74 // BLUE_GEM
        CHES 0xCA346C 0x45 // LUNA
        CHES 0xCA3478 0x31 // BRAVE_BOW
        CHES 0xCA3484 0x58 // BARRIER
        CHES 0xCA3490 0x67 // GUIDING_RING

        // Chapter 21: New Resolve
        Unit 0xCCFF0C 0x63 0x4F 2 |> difficulty Normal // HERO_CREST
        Unit 0xCD014C 0x63 0x4F 2 |> difficulty Hard // HERO_CREST
        Unit 0xCD003C 0x63 0x4F 2 |> difficulty HectorNormal // HERO_CREST
        Unit 0xCD02FC 0x63 0x4F 2 |> difficulty HectorHard // HERO_CREST
        ITGV 0xCB4E1C 0x0F // WYRMSLAYER
        ITGV 0xCB4E5C 0x7A // LIGHT_RUNE
        ITGV 0xCB4E9C 0x66 // ELYSIAN_WHIP
        ITGV 0xCB4EDC 0x4F // RESTORE

        // Chapter 22: Kinship's Bond
        Unit 0xCD1064 0x5A 0x34 2 // ANGELIC_ROBE
        Unit 0xCD0A44 0x64 0xD7 1 |> difficulty Normal // KNIGHT_CREST
        Unit 0xCD0C14 0x64 0x50 1 |> difficulty Hard // KNIGHT_CREST
        Unit 0xCD0B24 0x64 0xD7 1 |> difficulty HectorNormal // KNIGHT_CREST
        Unit 0xCD0D74 0x64 0x50 1 |> difficulty HectorHard // KNIGHT_CREST
        CHES 0xCA3AAC 0x23 // BRAVE_AXE

        // Chapter 23: Living Legend
        Unit 0xCD1CAC 0x67 0x54 2 |> difficulty Normal // GUIDING_RING
        Unit 0xCD1FFC 0x67 0xE8 1 |> difficulty Hard // GUIDING_RING
        Unit 0xCD1DFC 0x67 0x54 2 |> difficulty HectorNormal // GUIDING_RING
        Unit 0xCD21DC 0x67 0xE8 1 |> difficulty HectorHard // GUIDING_RING
        Sand 0xCB5CEC 0x63 // HERO_CREST
        Sand 0xCB5D2C 0x10 // LIGHT_BRAND
        Sand 0xCB5D6C 0x7C // FILLA_MIGHT
        Sand 0xCB5DAC 0x47 // ECLIPSE
        Sand 0xCB5DEC 0x96 // OCEAN_SEAL
        Sand 0xCB5E2C 0x62 // BODY_RING

        // Chapter 23x: Genesis
        CHES 0xCA3E3C 0x07 // SILVER_BLADE
        CHES 0xCA3E48 0x5C // SECRET_BOOK
        CHES 0xCA3E54 0x52 // BERSERK
        Chap 0xCB6118 0x88 |> route Tactician // AFA_DROPS

        // Chapter 24: Four Fanged Offense (Lloyd)
        yield! List.map (route Lloyd) [
            Unit 0xCD3128 0x65 0x6E 1 |> difficulty Normal // ORION_BOLT
            Unit 0xCD3298 0x65 0x6E 1 |> difficulty HectorNormal // ORION_BOLT
            ITGV 0xCB6DA8 0x87 // EARTH_SEAL
            ITGV 0xCB6DE8 0x50 // SILENCE
        ]

        // Chapter 24: Four Fanged Offense (Linus)
        yield! List.map (route Linus) [
            ITGV 0xCB77E4 0x50 // SILENCE
            ITGV 0xCB7824 0x65 // ORION_BOLT
            ITGV 0xCB7864 0x87 // EARTH_SEAL
        ]

        // Chapter 25: Crazed Beast
        yield! List.map (route Hector) [
            ITGV 0xCB830C 0x66 // ELYSIAN_WHIP
        ]

        // Chapter 26: Unfulfilled Heart
        Chap 0xCB87F8 0x89 // HEAVEN_SEAL - only if Hawkeye got to Four Fanged Offense
        Unit 0xCD568C 0x64 0x6D 1 |> difficulty Normal // KNIGHT_CREST
        Unit 0xCD599C 0x64 0x6D 1 |> difficulty Hard // KNIGHT_CREST
        Unit 0xCD580C 0x64 0x6D 1 |> difficulty HectorNormal // KNIGHT_CREST
        ITGV 0xCB8974 0x56 // HAMMERNE

        // Chapter 27: Pale Flower of Darkness (Kenneth)
        yield! List.map (route Kenneth) [
            Unit 0xCD79A4 0x5D 0x7C 1 |> difficulty Hard // SPEEDWINGS
            Unit 0xCD7BC4 0x5D 0x7C 1 |> difficulty HectorHard // SPEEDWINGS
            CHES 0xCA4CB8 0x67 // GUIDING_RING
            CHES 0xCA4CC4 0x74 // BLUE_GEM
            CHES 0xCA4CD0 0x60 // TALISMAN
        ]

        // Chapter 27: Pale Flower of Darkness (Jerme)
        yield! List.map (route Jerme) [
            CHES 0xCA5024 0x60 // TALISMAN
            CHES 0xCA5030 0x73 // WHITE_GEM
            CHES 0xCA503C 0x63 // HERO_CREST
            CHES 0xCA5048 0x3A // BOLTING
        ]

        // Chapter 28: Battle Before Dawn
        Unit 0xCD87C8 0x66 0x6E 2 |> difficulty Normal // ELYSIAN_WHIP
        Unit 0xCD8828 0x66 0x6E 2 |> difficulty HectorNormal // ELYSIAN_WHIP
        Unit 0xCD90A8 0x87 0x6D 1 // EARTH_SEAL
        CHES 0xCA5418 0x61 // BOOTS
        CHES 0xCA5424 0x19 // BRAVE_LANCE
        CHES 0xCA5430 0x54 // RESCUE
        CHES 0xCA543C 0x70 // DELPHI_SHIELD
        ChTo 0xCBA90C 0x89 0x01 // HEAVEN_SEAL

        // Chapter 28x: Night of Farewells
        Unit 0xCD96B0 0x8B 0x5B 2 |> difficulty Normal // FELL_CONTRACT
        Unit 0xCD9A90 0x8B 0x5B 2 |> difficulty Hard // FELL_CONTRACT
        Unit 0xCD9880 0x8B 0x5B 2 |> difficulty HectorNormal // FELL_CONTRACT
        Unit 0xCD9CE0 0x8B 0x5B 2 |> difficulty HectorHard // FELL_CONTRACT
        Unit 0xCD96F0 0x5A 0x70 2 |> difficulty Normal // ANGELIC_ROBE
        Unit 0xCD9AD0 0x5A 0x70 2 |> difficulty Hard // ANGELIC_ROBE
        Unit 0xCD98C0 0x5A 0x70 2 |> difficulty HectorNormal // ANGELIC_ROBE
        Unit 0xCD9D20 0x5A 0x70 2 |> difficulty HectorHard // ANGELIC_ROBE
        CHES 0xCA58EC 0x48 // FENRIR
        CHES 0xCA58F8 0x7E // THOR_IRE
        CHES 0xCA5904 0x4C // RECOVER
        CHES 0xCA5910 0x5D // SPEEDWINGS

        // Chapter 28 (Eliwood): Valorous Roland

        // Chapter 29: Cog of Destiny
        Unit 0xCDAB18 0x67 0x70 1 |> difficulty Normal // GUIDING_RING
        Unit 0xCDB078 0x65 0x70 1 |> difficulty Hard // ORION_BOLT
        Unit 0xCDADC8 0x67 0x70 1 |> difficulty HectorNormal // GUIDING_RING
        ITGV 0xCBBFFC 0x53 // WARP

        // Chapter 30 (Hector): The Berserker
        yield! List.map (route Hector) [
            Unit 0xCDC5B4 0x5D 0xEA 1 |> difficulty HectorHard // SPEEDWINGS
            Unit 0xCDC644 0x5E 0xEA 1 |> difficulty HectorHard // GODDESS_ICON
            CHES 0xCA5B90 0x6D // PURE_WATER
            CHES 0xCA5B9C 0x6C // ELIXIR
            CHES 0xCA5BA8 0x8D // WOLF_BEIL
        ]

        // Chapter 31: Sands of Time
        CHES 0xCA5EA8 0x73 // WHITE_GEM
        CHES 0xCA5EB4 0x5F // DRAGONSHIELD
        CHES 0xCA5EC0 0x62 // BODY_RING

        // Chapter 31x: Battle Preparations

        // Chapter 32: Victory or Death
        Chap 0xCBE9A0 0x87 // EARTH_SEAL
        ITGV 0xCBEC54 0x7F // SET_LITANY
        ITGV 0xCBECE4 0x60 // TALISMAN

        // Chapter 32x: The Value of Life
        yield! List.map (route Hector) [
            CHES 0xCA6518 0x4E // FORTIFY
            CHES 0xCA6524 0x11 // RUNE_SWORD
        ]

        // Final: Light (Part 1)
        CHES 0xCA66C4 0x6C // ELIXIR
        CHES 0xCA66D0 0x4E // FORTIFY
        CHES 0xCA66DC 0x58 // BARRIER
        CHES 0xCA66E8 0x61 // BOOTS

        // Final: Light (Part 2)
        CHES 0xCA6798 0x0F // WYRMSLAYER
        CHES 0xCA67A4 0x56 // HAMMERNE
    ]