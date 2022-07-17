namespace RewardRandomizer

open Item
open Reward

module FE6 =
    let Items = [
        item 0x0 "NONE" Other
        item 0x1 "IRON_SWORD" Other
        item 0x2 "IRON_BLADE" Other
        item 0x3 "STEEL_SWORD" Other
        item 0x4 "SILVER_SWORD" Other
        item 0x5 "SLIM_SWORD" Other
        item 0x6 "POISON_SWORD" Other
        item 0x7 "BRAVE_SWORD" Other
        item 0x8 "LIGHT_BRAND" Other
        item 0xa "ARMORSLAYER" Other
        item 0xb "RAPIER" Other
        item 0xc "KILLING_EDGE" Other
        item 0xd "LANCEREAVER" Other
        item 0xe "WO_DAO" Other
        item 0x72 "STEEL_BLADE" Other
        item 0x73 "SILVER_BLADE" Other
        item 0x74 "AL_SWORD" Other
        item 0x78 "WYRMSLAYER" Other
        item 0x7d "RUNE_SWORD" Other
        item 0x10 "IRON_LANCE" Other
        item 0x11 "STEEL_LANCE" Other
        item 0x12 "SILVER_LANCE" Other
        item 0x13 "SLIM_LANCE" Other
        item 0x14 "POISON_LANCE" Other
        item 0x15 "BRAVE_LANCE" Other
        item 0x16 "JAVELIN" Other
        item 0x18 "HORSESLAYER" Other
        item 0x19 "KILLER_LANCE" Other
        item 0x1a "AXEREAVER" Other
        item 0x75 "GANT_LANCE" Other
        item 0x7e "SPEAR" Other
        item 0x1b "IRON_AXE" Other
        item 0x1c "STEEL_AXE" Other
        item 0x1d "SILVER_AXE" Other
        item 0x1e "POISON_AXE" Other
        item 0x1f "BRAVE_AXE" Other
        item 0x20 "HAND_AXE" Other
        item 0x22 "HAMMER" Other
        item 0x23 "KILLER_AXE" Other
        item 0x24 "SWORDREAVER" Other
        item 0x25 "DEVIL_AXE" Other
        item 0x26 "HALBERD" Other
        item 0x7f "TOMAHAWK" Other
        item 0x27 "IRON_BOW" Other
        item 0x28 "STEEL_BOW" Other
        item 0x29 "SILVER_BOW" Other
        item 0x2a "POISON_BOW" Other
        item 0x2b "KILLER_BOW" Other
        item 0x2c "BRAVE_BOW" Other
        item 0x2d "SHORT_BOW" Other
        item 0x2e "LONGBOW" Other
        item 0x33 "FIRE" Other
        item 0x34 "THUNDER" Other
        item 0x35 "FIMBULVETR" Other
        item 0x36 "ELFIRE" Other
        item 0x37 "AIRCALIBUR" Other
        item 0x39 "BOLTING" Other
        item 0x3b "LIGHTNING" Other
        item 0x3c "DIVINE" Other
        item 0x3d "PURGE" Other
        item 0x3f "FLUX" Other
        item 0x40 "NOSFERATU" Other
        item 0x41 "ECLIPSE" Other
        item 0x38 "FENRIR" Other
        item 0x9 "DURANDAL" Other
        item 0x17 "MALTET" Other
        item 0x21 "ARMADS" Other
        item 0x2f "MURGLEIS" Other
        item 0x3a "FORBLAZE" Other
        item 0x3e "AUREOLA" Other
        item 0x42 "APOCALYPSE" Other
        item 0x43 "HEAL" Other
        item 0x44 "MEND" Other
        item 0x45 "RECOVER" Other
        item 0x46 "PHYSIC" Other
        item 0x47 "FORTIFY" Other
        item 0x48 "WARP" Other
        item 0x49 "RESCUE" Other
        item 0x4a "RESTORE" Other
        item 0x4b "SILENCE" Other
        item 0x4c "SLEEP" Other
        item 0x4d "TORCH_STAFF" Other
        item 0x4e "HAMMERNE" Other
        item 0x50 "BERSERK" Other
        item 0x51 "UNLOCK" Other
        item 0x52 "BARRIER" Other
        item 0x76 "TINA_STAFF" Other
        item 0x77 "HOLY_MAIDEN" Other
        item 0x4f "UNUSED_WATCH_STAFF" Other
        item 0x53 "FIRE_DRAGON_STONE" Other
        item 0x54 "DIVINE_DRAGON_STONE" Other
        item 0x55 "MAGIC_DRAGON_STONE" Other
        item 0x56 "SECRET_BOOK" StatBooster
        item 0x57 "GODDESS_ICON" StatBooster
        item 0x58 "ANGELIC_ROBE" StatBooster
        item 0x59 "DRAGON_SHIELD" StatBooster
        item 0x5a "ENERGY_RING" StatBooster
        item 0x5b "SPEEDWING" StatBooster
        item 0x5c "TALISMAN" StatBooster
        item 0x5d "BOOTS" StatBooster
        item 0x5e "BODY_RING" StatBooster
        item 0x5f "HERO_CREST" Promotion
        item 0x60 "KNIGHT_CREST" Promotion
        item 0x61 "ORION_BOLT" Promotion
        item 0x62 "ELYSIAN_WHIP" Promotion
        item 0x63 "GUIDING_RING" Promotion
        item 0x64 "CHEST_KEY_5" Other
        item 0x65 "DOOR_KEY" Other
        item 0x67 "LOCKPICK" Other
        item 0x68 "VULNERARY" Other
        item 0x69 "ELIXIR" Other
        item 0x6a "PURE_WATER" Other
        item 0x6b "TORCH" Other
        item 0x6c "ANTITOXIN" Other
        item 0x6d "MEMBER_CARD" Other
        item 0x6e "SILVER_CARD" Other
        item 0x79 "WHITE_GEM" Other
        item 0x7a "BLUE_GEM" Other
        item 0x7b "RED_GEM" Other
        item 0x7c "DELPHI_SHIELD" Other
        item 0x30 "BALLISTA" Other
        item 0x31 "IRON_BALLISTA" Other
        item 0x32 "KILLER_BALLISTA" Other
        item 0xf "BINDING_BLADE" Other
        item 0x70 "DARK_BREATH" Other
        item 0x71 "ECKESACHS" Other
    ]

    let CHES offset item = reward Chest (offset + 4) item 0
    let ITGV offset item = reward Village (offset + 4) item 0
    let ITGC offset item unit = reward FE6Story (offset + 8) item unit
    let Unit offset item unit pos = reward StartingInventory (offset + 8 + pos) item unit

    let JP = [
        // Fire Emblem - Fuuin no Tsurugi (Japan)

        // Chapter 1: Dawn of Destiny

        // Chapter 2: The Princess of Bern
        ITGV 0x66BD40 0x0A // ARMORSLAYER

        // Chapter 3: Late Arrival
        CHES 0x667928 0x26 // HALBERD
        ITGV 0x66C2A0 0x44 // MEND

        // Chapter 4: Collapse of the Alliance
        ITGV 0x66C718 0x58 // ANGELIC_ROBE
        ITGV 0x66C688 0x65 // DOOR_KEY
        ITGV 0x66C6D0 0x72 // STEEL_BLADE

        // Chapter 5: The Emblem of Fire
        ITGV 0x66CA14 0x75 // GANT_LANCE

        // Chapter 6: The Trap
        CHES 0x667C64 0x2D // SHORT_BOW
        CHES 0x667C70 0x57 // GODDESS_ICON
        CHES 0x667C7C 0x23 // KILLER_AXE
        CHES 0x667C94 0x12 // SILVER_LANCE
        CHES 0x667CA0 0x51 // UNLOCK

        // Chapter 7: The Rebellion of Ostia
        CHES 0x667F0C 0x0B // RAPIER
        CHES 0x667F18 0x52 // BARRIER
        ITGV 0x66D2D8 0x4D // TORCH_STAFF
        ITGV 0x66D320 0x69 // ELIXIR
        ITGV 0x66D368 0x7B // RED_GEM
        ITGV 0x66D3B0 0x46 // PHYSIC
        ITGV 0x66D3F8 0x0C // KILLING_EDGE
        ITGV 0x66D440 0x5F // HERO_CREST
        ITGV 0x66D488 0x18 // HORSESLAYER
        ITGV 0x66D290 0x2E // LONGBOW

        // Chapter 8: The Reunion
        CHES 0x668070 0x62 // ELYSIAN_WHIP
        CHES 0x66807C 0x1D // SILVER_AXE
        CHES 0x668088 0x56 // SECRET_BOOK
        CHES 0x668094 0x63 // GUIDING_RING
        CHES 0x6680A0 0x08 // LIGHT_BRAND
        CHES 0x6680AC 0x2B // KILLER_BOW
        CHES 0x6680B8 0x36 // ELFIRE
        CHES 0x6680C4 0x60 // KNIGHT_CREST
        ITGV 0x66D6D8 0x34 // THUNDER

        // Chapter 8x: The Blazing Sword
        ITGC 0x674C38 0x09 0x01 // DURANDAL

        // Chapter 9: The Misty Isles
        ITGV 0x66DDDC 0x4A // RESTORE
        ITGV 0x66DE2C 0x2B // KILLER_BOW
        ITGV 0x66DD94 0x04 // SILVER_SWORD

        yield! List.map (route Echidna) [
            // Chapter 10A: The Resistance Forces
            ITGV 0x66E260 0x24 // SWORDREAVER
            ITGC 0x66E2E4 0x78 0x01 // WYRMSLAYER

            // Chapter 11A: The Hero of the Western Isles
            ITGV 0x66EADC 0x5B // SPEEDWING
            ITGV 0x66EB24 0x58 // ANGELIC_ROBE
            ITGV 0x66EB6C 0x61 // ORION_BOLT
            ITGV 0x66EBB4 0x1A // AXEREAVER
            ITGV 0x66E9BC 0x59 // DRAGON_SHIELD
            ITGV 0x66EA04 0x4C // SLEEP
            ITGV 0x66EA94 0x4A // RESTORE
            ITGC 0x66ECE8 0x5F 0x01 // HERO_CREST
            ITGC 0x66ED4C 0x61 0x0F // ORION_BOLT
            ITGC 0x66EDB0 0x62 0x2E // ELYSIAN_WHIP
        ]

        yield! List.map (route Bartre) [
            // Chapter 10B: Caught in the Middle
            ITGV 0x673658 0x59 // DRAGON_SHIELD
            ITGV 0x673420 0x4C // SLEEP
            ITGV 0x6736A0 0x24 // SWORDREAVER
            ITGV 0x673468 0x61 // ORION_BOLT
            ITGV 0x6735BC 0x69 |> tag "Western" // ELIXIR
            ITGV 0x673604 0x5B |> tag "Western" // SPEEDWING
            ITGV 0x6734B0 0x52 // BARRIER
            ITGV 0x67350C 0x65 |> tag "Eastern" // DOOR_KEY
            ITGV 0x673554 0x24 |> tag "Eastern" // SWORDREAVER
            ITGC 0x673808 0x61 0x0F // ORION_BOLT
            ITGC 0x6737AC 0x5F 0x01 // HERO_CREST
            ITGC 0x673734 0x78 0x01 // WYRMSLAYER
            ITGC 0x673864 0x62 0x2E // ELYSIAN_WHIP

            // Chapter 11B: Escape to Freedom
            Unit 0x6826A4 0x58 0x34 1 // ANGELIC_ROBE
            ITGV 0x673BE8 0x7B // RED_GEM
            ITGC 0x673CB0 0x5A 0x01 // ENERGY_RING
        ]

        // Chapter 12: The True Enemy
        CHES 0x668678 0x3F // FLUX
        CHES 0x668684 0x37 // AIRCALIBUR
        CHES 0x668690 0x7A // BLUE_GEM
        CHES 0x66869C 0x62 // ELYSIAN_WHIP
        CHES 0x6686A8 0x2C // BRAVE_BOW
        CHES 0x6686B4 0x0D // LANCEREAVER

        // Chapter 12x: The Axe of Thunder
        CHES 0x669FE4 0x69 // ELIXIR
        CHES 0x669FF0 0x6C // ANTITOXIN
        CHES 0x669FFC 0x6B // TORCH
        CHES 0x66A008 0x6C // ANTITOXIN
        CHES 0x66A014 0x69 // ELIXIR
        CHES 0x66A020 0x67 // LOCKPICK
        CHES 0x66A02C 0x7B // RED_GEM
        CHES 0x66A038 0x6C // ANTITOXIN
        CHES 0x66A044 0x6C // ANTITOXIN
        CHES 0x66A050 0x64 // CHEST_KEY_5
        CHES 0x66A05C 0x69 // ELIXIR
        CHES 0x66A068 0x79 // WHITE_GEM
        ITGC 0x67504C 0x21 0x01 // ARMADS

        // Chapter 13: The Rescue Plan
        ITGV 0x66F9F8 0x5E // BODY_RING
        ITGV 0x66FA40 0x74 // AL_SWORD
        ITGC 0x66FBCC 0x60 0x1D |> tag "Percival Knight Crest" // KNIGHT_CREST

        // Chapter 14: Arcadia
        ITGV 0x66FE44 0x4B // SILENCE
        ITGV 0x66FE84 0x5C // TALISMAN
        ITGV 0x66FEC4 0x5D // BOOTS
        ITGV 0x66FF04 0x73 // SILVER_BLADE
        ITGV 0x66FF44 0x5B // SPEEDWING
        ITGV 0x66FF84 0x6E // SILVER_CARD
        ITGV 0x66FFC4 0x48 // WARP
        ITGV 0x670018 0x63 // GUIDING_RING

        // Chapter 14x: The Infernal Element
        ITGC 0x6752F4 0x3A 0x01 // FORBLAZE

        // Chapter 15: The Dragon Girl
        ITGV 0x67045C 0x4E // HAMMERNE
        ITGV 0x670414 0x3C // DIVINE
        ITGC 0x670590 0x60 0x1D |> tag "Percival Knight Crest" // KNIGHT_CREST

        // Chapter 16: Retaking the Capital
        CHES 0x668BC0 0x45 // RECOVER
        CHES 0x668BCC 0x4A // RESTORE
        CHES 0x668BD8 0x15 // BRAVE_LANCE
        CHES 0x668BE4 0x39 // BOLTING
        CHES 0x668BFC 0x49 // RESCUE
        CHES 0x668C08 0x29 // SILVER_BOW
        CHES 0x668C14 0x5F // HERO_CREST
        CHES 0x668C20 0x60 // KNIGHT_CREST
        CHES 0x668C2C 0x50 // BERSERK

        // Chapter 16x: The Pinnacle of Light
        ITGC 0x675514 0x3E 0x01 // AUREOLA

        yield! List.map (route Ilia) [
            // Chapter 17A: The Path Through the Ocean
            ITGV 0x670DEC 0x76 // TINA_STAFF

            // Chapter 18A: The Frozen River
            ITGV 0x6711DC 0x56 // SECRET_BOOK
            ITGV 0x67114C 0x57 // GODDESS_ICON
            ITGV 0x671194 0x63 // GUIDING_RING

            // Chapter 19A: Bitter Cold
            Unit 0x67FC78 0x60 0x9A 1 // KNIGHT_CREST
            ITGV 0x671498 0x37 // AIRCALIBUR
            ITGV 0x6714E0 0x5A // ENERGY_RING

            // Chapter 20A: The Liberation of Ilia
            Unit 0x680144 0x62 0xAD 1 // ELYSIAN_WHIP
            CHES 0x669114 0x5B // SPEEDWING
            CHES 0x669120 0x1A // AXEREAVER
            CHES 0x66912C 0x2E // LONGBOW
            CHES 0x669138 0x0E // WO_DAO
            CHES 0x669144 0x3D // PURGE
            CHES 0x669150 0x4C // SLEEP
            CHES 0x66915C 0x40 // NOSFERATU
            CHES 0x669168 0x78 // WYRMSLAYER
            ITGC 0x671A4C 0x58 0x01 |> tag "Ilia angelic robe" // ANGELIC_ROBE
            ITGC 0x671B08 0x58 0x01 |> tag "Ilia angelic robe" // ANGELIC_ROBE

            // Chapter 20Ax: The Spear of Ice
            ITGC 0x6756E0 0x17 0x01 // MALTET

            // Chapter 21: The Binding Blade
            Unit 0x680764 0x2F 0x12 2 // MURGLEIS
            Unit 0x680764 0x77 0x12 3 // HOLY_MAIDEN
        ]

        yield! List.map (route Sacae) [
            // Chapter 17B: The Bishop's Teaching
            ITGV 0x673DFC 0x76 // TINA_STAFF
            ITGV 0x673E44 0x41 // ECLIPSE

            // Chapter 18B: The Laws of Sacae

            // Chapter 19B: Battle in Bulgar
            ITGV 0x67463C 0x78 // WYRMSLAYER
            ITGV 0x67475C 0x2E // LONGBOW
            ITGV 0x674684 0x5A // ENERGY_RING
            ITGV 0x6746CC 0x57 // GODDESS_ICON
            ITGV 0x674714 0x0E // WO_DAO

            // Chapter 20B: The Silver Wolf
            Unit 0x6840C4 0x61 0xAD 1 // ORION_BOLT
            CHES 0x669ED4 0x4C // SLEEP
            CHES 0x669EE0 0x1A // AXEREAVER
            CHES 0x669EEC 0x63 // GUIDING_RING
            CHES 0x669EF8 0x7A // BLUE_GEM
            CHES 0x669F04 0x40 // NOSFERATU
            CHES 0x669F10 0x59 // DRAGON_SHIELD

            // Chapter 20Bx: The Bow of the Winds
            ITGC 0x675B1C 0x2F 0x01 // MURGLEIS

            // Chapter 21: The Binding Blade
            Unit 0x680744 0x17 0x12 2 // MALTET
            Unit 0x680744 0x77 0x12 3 // HOLY_MAIDEN
        ]

        // Chapter 21: The Binding Blade
        Unit 0x680454 0x60 0x62 1 // KNIGHT_CREST
        ITGV 0x671F40 0x60 // KNIGHT_CREST
        ITGC 0x6720A4 0x0F 0x01 |> tag "Binding Blade" // BINDING_BLADE

        // Chapter 21x: The Silencing Darkness
        ITGV 0x675D04 0x69 // ELIXIR
        ITGV 0x675E14 0x69 // ELIXIR
        ITGV 0x675DD0 0x69 // ELIXIR
        ITGV 0x675D8C 0x69 // ELIXIR
        ITGV 0x675D48 0x69 // ELIXIR
        ITGV 0x675E58 0x69 // ELIXIR
        ITGC 0x675FB0 0x0F 0x01 |> tag "Binding Blade" // BINDING_BLADE
        ITGC 0x675EC8 0x42 0x01 // APOCALYPSE

        // Chapter 22: The Neverending Dream
        Unit 0x680F50 0x5F 0x64 1 // HERO_CREST
        CHES 0x669430 0x4C // SLEEP
        CHES 0x66943C 0x79 // WHITE_GEM
        CHES 0x669448 0x40 // NOSFERATU
        CHES 0x669454 0x78 // WYRMSLAYER
        CHES 0x669460 0x59 // DRAGON_SHIELD
        CHES 0x66946C 0x24 // SWORDREAVER

        // Chapter 23: The Ghost of Bern
        Unit 0x6815C0 0x63 0x63 2 // GUIDING_RING

        // Chapter 24: The Truth of the Legend

        // Final: Beyond Darkness
    ]

    let FE6Localization = [
        // Most of the offsets are the same as the original Japanese version, but translation patches have moved a few of them.

        // Chapter 1: Dawn of Destiny

        // Chapter 2: The Princess of Bern
        ITGV 0x66BD40 0x0A // ARMORSLAYER

        // Chapter 3: Late Arrival
        CHES 0x667928 0x26 // HALBERD
        ITGV 0x66C2A0 0x44 // MEND

        // Chapter 4: Collapse of the Alliance
        ITGV 0x66C718 0x58 // ANGELIC_ROBE
        ITGV 0x66C688 0x65 // DOOR_KEY
        ITGV 0x66C6D0 0x72 // STEEL_BLADE

        // Chapter 5: The Emblem of Fire
        ITGV 0x66CA14 0x75 // GANT_LANCE

        // Chapter 6: The Trap
        CHES 0x667C64 0x2D // SHORT_BOW
        CHES 0x667C70 0x57 // GODDESS_ICON
        CHES 0x667C7C 0x23 // KILLER_AXE
        CHES 0x667C94 0x12 // SILVER_LANCE
        CHES 0x667CA0 0x51 // UNLOCK

        // Chapter 7: The Rebellion of Ostia
        CHES 0x667F0C 0x0B // RAPIER
        CHES 0x667F18 0x52 // BARRIER
        ITGV 0x66D2D8 0x4D // TORCH_STAFF
        ITGV 0x66D320 0x69 // ELIXIR
        ITGV 0x66D368 0x7B // RED_GEM
        ITGV 0x66D3B0 0x46 // PHYSIC
        ITGV 0x66D3F8 0x0C // KILLING_EDGE
        ITGV 0x66D440 0x5F // HERO_CREST
        ITGV 0x66D488 0x18 // HORSESLAYER
        ITGV 0x66D290 0x2E // LONGBOW

        // Chapter 8: The Reunion
        CHES 0x668070 0x62 // ELYSIAN_WHIP
        CHES 0x66807C 0x1D // SILVER_AXE
        CHES 0x668088 0x56 // SECRET_BOOK
        CHES 0x668094 0x63 // GUIDING_RING
        CHES 0x6680A0 0x08 // LIGHT_BRAND
        CHES 0x6680AC 0x2B // KILLER_BOW
        CHES 0x6680B8 0x36 // ELFIRE
        CHES 0x6680C4 0x60 // KNIGHT_CREST
        ITGV 0x66D6D8 0x34 // THUNDER

        // Chapter 8x: The Blazing Sword
        ITGC 0x674C38 0x09 0x01 // DURANDAL

        // Chapter 9: The Misty Isles
        ITGV 0x66DDDC 0x4A // RESTORE
        ITGV 0x66DE2C 0x2B // KILLER_BOW
        ITGV 0x66DD94 0x04 // SILVER_SWORD

        yield! List.map (route Echidna) [
            // Chapter 10A: The Resistance Forces
            ITGV 0x66E260 0x24 // SWORDREAVER
            ITGC 0x66E2E4 0x78 0x01 // WYRMSLAYER

            // Chapter 11A: The Hero of the Western Isles
            ITGV 0x66EADC 0x5B // SPEEDWING
            ITGV 0x66EB24 0x58 // ANGELIC_ROBE
            ITGV 0x66EB6C 0x61 // ORION_BOLT
            ITGV 0x66EBB4 0x1A // AXEREAVER
            ITGV 0x66E9BC 0x59 // DRAGON_SHIELD
            ITGV 0x66EA04 0x4C // SLEEP
            ITGV 0x66EA94 0x4A // RESTORE
            ITGC 0x800AA0 0x61 0x0F // ORION_BOLT
            ITGC 0x800A3C 0x5F 0x01 // HERO_CREST
            ITGC 0x800B04 0x62 0x2E // ELYSIAN_WHIP
        ]

        yield! List.map (route Bartre) [
            // Chapter 10B: Caught in the Middle
            ITGV 0x673658 0x59 // DRAGON_SHIELD
            ITGV 0x673420 0x4C // SLEEP
            ITGV 0x6736A0 0x24 // SWORDREAVER
            ITGV 0x673468 0x61 // ORION_BOLT
            ITGV 0x6735BC 0x69 |> special // ELIXIR
            ITGV 0x673604 0x5B |> special // SPEEDWING
            ITGV 0x6734B0 0x52 // BARRIER
            ITGV 0x67350C 0x65 |> special // DOOR_KEY
            ITGV 0x673554 0x24 |> special // SWORDREAVER
            ITGC 0x8003A0 0x78 0x01 // WYRMSLAYER
            ITGC 0x8004D0 0x62 0x2E // ELYSIAN_WHIP
            ITGC 0x800474 0x61 0x0F // ORION_BOLT
            ITGC 0x800418 0x5F 0x01 // HERO_CREST

            // Chapter 11B: Escape to Freedom
            Unit 0x6826A4 0x58 0x34 1 // ANGELIC_ROBE
            ITGV 0x673BE8 0x7B // RED_GEM
            ITGC 0x673CB0 0x5A 0x01 // ENERGY_RING
        ]

        // Chapter 12: The True Enemy
        CHES 0x668678 0x3F // FLUX
        CHES 0x668684 0x37 // AIRCALIBUR
        CHES 0x668690 0x7A // BLUE_GEM
        CHES 0x66869C 0x62 // ELYSIAN_WHIP
        CHES 0x6686A8 0x2C // BRAVE_BOW
        CHES 0x6686B4 0x0D // LANCEREAVER

        // Chapter 12x: The Axe of Thunder
        CHES 0x669FE4 0x69 // ELIXIR
        CHES 0x669FF0 0x6C // ANTITOXIN
        CHES 0x669FFC 0x6B // TORCH
        CHES 0x66A008 0x6C // ANTITOXIN
        CHES 0x66A014 0x69 // ELIXIR
        CHES 0x66A020 0x67 // LOCKPICK
        CHES 0x66A02C 0x7B // RED_GEM
        CHES 0x66A038 0x6C // ANTITOXIN
        CHES 0x66A044 0x6C // ANTITOXIN
        CHES 0x66A050 0x64 // CHEST_KEY_5
        CHES 0x66A05C 0x69 // ELIXIR
        CHES 0x66A068 0x79 // WHITE_GEM
        ITGC 0x67504C 0x21 0x01 // ARMADS

        // Chapter 13: The Rescue Plan
        ITGV 0x66F9F8 0x5E // BODY_RING
        ITGV 0x66FA40 0x74 // AL_SWORD
        ITGC 0x66FBCC 0x60 0x1D |> tag "Percival Knight Crest" // KNIGHT_CREST

        // Chapter 14: Arcadia
        ITGV 0x66FE44 0x4B // SILENCE
        ITGV 0x66FE84 0x5C // TALISMAN
        ITGV 0x66FEC4 0x5D // BOOTS
        ITGV 0x66FF04 0x73 // SILVER_BLADE
        ITGV 0x66FF44 0x5B // SPEEDWING
        ITGV 0x66FF84 0x6E // SILVER_CARD
        ITGV 0x66FFC4 0x48 // WARP
        ITGV 0x670018 0x63 // GUIDING_RING

        // Chapter 14x: The Infernal Element
        ITGC 0x6752F4 0x3A 0x01 // FORBLAZE

        // Chapter 15: The Dragon Girl
        ITGV 0x67045C 0x4E // HAMMERNE
        ITGV 0x670414 0x3C // DIVINE
        ITGC 0x670590 0x60 0x1D |> tag "Percival Knight Crest" // KNIGHT_CREST

        // Chapter 16: Retaking the Capital
        CHES 0x668BC0 0x45 // RECOVER
        CHES 0x668BCC 0x4A // RESTORE
        CHES 0x668BD8 0x15 // BRAVE_LANCE
        CHES 0x668BE4 0x39 // BOLTING
        CHES 0x668BFC 0x49 // RESCUE
        CHES 0x668C08 0x29 // SILVER_BOW
        CHES 0x668C14 0x5F // HERO_CREST
        CHES 0x668C20 0x60 // KNIGHT_CREST
        CHES 0x668C2C 0x50 // BERSERK

        // Chapter 16x: The Pinnacle of Light
        ITGC 0x675514 0x3E 0x01 // AUREOLA

        yield! List.map (route Ilia) [
            // Chapter 17A: The Path Through the Ocean
            ITGV 0x670DEC 0x76 // TINA_STAFF

            // Chapter 18A: The Frozen River
            ITGV 0x6711DC 0x56 // SECRET_BOOK
            ITGV 0x67114C 0x57 // GODDESS_ICON
            ITGV 0x671194 0x63 // GUIDING_RING

            // Chapter 19A: Bitter Cold
            Unit 0x67FC78 0x60 0x9A 1 // KNIGHT_CREST
            ITGV 0x671498 0x37 // AIRCALIBUR
            ITGV 0x6714E0 0x5A // ENERGY_RING

            // Chapter 20A: The Liberation of Ilia
            Unit 0x680144 0x62 0xAD 1 // ELYSIAN_WHIP
            CHES 0x669114 0x5B // SPEEDWING
            CHES 0x669120 0x1A // AXEREAVER
            CHES 0x66912C 0x2E // LONGBOW
            CHES 0x669138 0x0E // WO_DAO
            CHES 0x669144 0x3D // PURGE
            CHES 0x669150 0x4C // SLEEP
            CHES 0x66915C 0x40 // NOSFERATU
            CHES 0x669168 0x78 // WYRMSLAYER
            ITGC 0x8305B8 0x58 0x01 |> tag "Ilia angelic robe" // ANGELIC_ROBE
            ITGC 0x83067C 0x58 0x01 |> tag "Ilia angelic robe" // ANGELIC_ROBE

            // Chapter 20Ax: The Spear of Ice
            ITGC 0x8001EC 0x17 0x01 // MALTET

            // Chapter 21: The Binding Blade
            Unit 0x680764 0x2F 0x12 2 // MURGLEIS
            Unit 0x680764 0x77 0x12 3 // HOLY_MAIDEN
        ]

        yield! List.map (route Sacae) [
            // Chapter 17B: The Bishop's Teaching
            ITGV 0x673DFC 0x76 // TINA_STAFF
            ITGV 0x673E44 0x41 // ECLIPSE

            // Chapter 18B: The Laws of Sacae

            // Chapter 19B: Battle in Bulgar
            ITGV 0x67463C 0x78 // WYRMSLAYER
            ITGV 0x67475C 0x2E // LONGBOW
            ITGV 0x674684 0x5A // ENERGY_RING
            ITGV 0x6746CC 0x57 // GODDESS_ICON
            ITGV 0x674714 0x0E // WO_DAO

            // Chapter 20B: The Silver Wolf
            Unit 0x6840C4 0x61 0xAD 1 // ORION_BOLT
            CHES 0x669ED4 0x4C // SLEEP
            CHES 0x669EE0 0x1A // AXEREAVER
            CHES 0x669EEC 0x63 // GUIDING_RING
            CHES 0x669EF8 0x7A // BLUE_GEM
            CHES 0x669F04 0x40 // NOSFERATU
            CHES 0x669F10 0x59 // DRAGON_SHIELD

            // Chapter 20Bx: The Bow of the Winds
            ITGC 0x800894 0x2F 0x01 // MURGLEIS

            // Chapter 21: The Binding Blade
            Unit 0x680744 0x17 0x12 2 // MALTET
            Unit 0x680744 0x77 0x12 3 // HOLY_MAIDEN
        ]

        // Chapter 21: The Binding Blade
        Unit 0x680454 0x60 0x62 1 // KNIGHT_CREST
        ITGV 0x671F40 0x60 // KNIGHT_CREST
        ITGC 0x6720A4 0x0F 0x01 |> tag "Binding Blade" // BINDING_BLADE

        // Chapter 21x: The Silencing Darkness
        ITGV 0x675D04 0x69 // ELIXIR
        ITGV 0x675E14 0x69 // ELIXIR
        ITGV 0x675DD0 0x69 // ELIXIR
        ITGV 0x675D8C 0x69 // ELIXIR
        ITGV 0x675D48 0x69 // ELIXIR
        ITGV 0x675E58 0x69 // ELIXIR
        ITGC 0x675EC8 0x42 0x01 // APOCALYPSE
        ITGC 0x675FB0 0x0F 0x01 |> tag "Binding Blade" // BINDING_BLADE

        // Chapter 22: The Neverending Dream
        Unit 0x680F50 0x5F 0x64 1 // HERO_CREST
        CHES 0x669430 0x4C // SLEEP
        CHES 0x66943C 0x79 // WHITE_GEM
        CHES 0x669448 0x40 // NOSFERATU
        CHES 0x669454 0x78 // WYRMSLAYER
        CHES 0x669460 0x59 // DRAGON_SHIELD
        CHES 0x66946C 0x24 // SWORDREAVER

        // Chapter 23: The Ghost of Bern
        Unit 0x6815C0 0x63 0x63 2 // GUIDING_RING

        // Chapter 24: The Truth of the Legend

        // Final: Beyond Darkness
    ]
