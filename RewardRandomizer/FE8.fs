namespace RewardRandomizer

open Item
open Reward

module FE8 =
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
        item 0xb "BRAVE_SWORD" Other
        item 0xc "SHAMSHIR" Other
        item 0xd "KILLING_EDGE" Other
        item 0xe "ARMORSLAYER" Other
        item 0xf "WYRMSLAYER" Other
        item 0x10 "LIGHT_BRAND" Other
        item 0x11 "RUNE_SWORD" Other
        item 0x12 "LANCEREAVER" Other
        item 0x13 "ZANBATO" Other
        item 0x81 "SHADOWKILLER" Other
        item 0x85 "SIEGLINDE" Other
        item 0x91 "AUDHULMA" Other
        item 0xa1 "WIND_SWORD" Other
        item 0x14 "IRON_LANCE" Other
        item 0x15 "SLIM_LANCE" Other
        item 0x16 "STEEL_LANCE" Other
        item 0x17 "SILVER_LANCE" Other
        item 0x18 "TOXIN_LANCE" Other
        item 0x19 "BRAVE_LANCE" Other
        item 0x1a "KILLER_LANCE" Other
        item 0x1b "HORSESLAYER" Other
        item 0x1c "JAVELIN" Other
        item 0x1d "SPEAR" Other
        item 0x1e "AXEREAVER" Other
        item 0x78 "REGINLEIF" Other
        item 0x82 "BRIGHT_LANCE" Other
        item 0x8d "DRAGONSPEAR" Other
        item 0x8e "VIDOFNIR" Other
        item 0x92 "SIEGMUND" Other
        item 0x95 "HEAVY_SPEAR" Other
        item 0x96 "SHORT_SPEAR" Other
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
        item 0x2c "HATCHET" Other
        item 0x5a "DRAGON_AXE" Other
        item 0x83 "FIENDCLEAVER" Other
        item 0x86 "BATTLE_AXE" Other
        item 0x93 "GARM" Other
        item 0x2d "IRON_BOW" Other
        item 0x2e "STEEL_BOW" Other
        item 0x2f "SILVER_BOW" Other
        item 0x30 "POISON_BOW" Other
        item 0x31 "KILLER_BOW" Other
        item 0x32 "BRAVE_BOW" Other
        item 0x33 "SHORT_BOW" Other
        item 0x34 "LONG_BOW" Other
        item 0x84 "BEACON_BOW" Other
        item 0x94 "NIDHOGG" Other
        item 0x38 "FIRE" Other
        item 0x39 "THUNDER" Other
        item 0x3a "ELFIRE" Other
        item 0x3b "BOLTING" Other
        item 0x3c "FIMBULVETR" Other
        item 0x3e "EXCALIBUR" Other
        item 0x3f "LIGHTNING" Other
        item 0x40 "SHINE" Other
        item 0x41 "DIVINE" Other
        item 0x42 "PURGE" Other
        item 0x43 "AURA" Other
        item 0x87 "IVALDI" Other
        item 0x45 "FLUX" Other
        item 0x46 "LUNA" Other
        item 0x47 "NOSFERATU" Other
        item 0x48 "ECLIPSE" Other
        item 0x49 "FENRIR" Other
        item 0x4a "GLEIPNIR" Other
        item 0x8f "NAGLFAR" Other
        item 0x4b "HEAL" Other
        item 0x4c "MEND" Other
        item 0x4d "RECOVER" Other
        item 0x4e "PHYSIC" Other
        item 0x4f "FORTIFY" Other
        item 0x8c "LATONA" Other
        item 0x50 "RESTORE" Other
        item 0x54 "WARP" Other
        item 0x55 "RESCUE" Other
        item 0x56 "TORCH_STAFF" Other
        item 0x57 "HAMMERNE" Other
        item 0x58 "UNLOCK" Other
        item 0x59 "BARRIER" Other
        item 0x51 "SILENCE" Other
        item 0x52 "SLEEP" Other
        item 0x53 "BERSERK" Other
        item 0x8b "SHARP_CLAW" Other
        item 0x90 "WRETCHED_AIR" Other
        item 0xaa "DRAGONSTONE" Other
        item 0xab "DEMON_SURGE" Other
        item 0xac "SHADOWSHOT" Other
        item 0xad "ROTTEN_CLAW" Other
        item 0xae "FETID_CLAW" Other
        item 0xaf "POISON_CLAW" Other
        item 0xb0 "LETHAL_TALON" Other
        item 0xb1 "FIERY_FANG" Other
        item 0xb2 "HELLFANG" Other
        item 0xb3 "EVIL_EYE" Other
        item 0xb4 "CRIMSON_EYE" Other
        item 0xb5 "STONE" Other
        item 0x5b "ANGELIC_ROBE" StatBooster
        item 0x5c "ENERGY_RING" StatBooster
        item 0x5d "SECRET_BOOK" StatBooster
        item 0x5e "SPEEDWINGS" StatBooster
        item 0x5f "GODDESS_ICON" StatBooster
        item 0x60 "DRAGONSHIELD" StatBooster
        item 0x61 "TALISMAN" StatBooster
        item 0x62 "BOOTS" Boots
        item 0x63 "BODY_RING" StatBooster
        item 0x64 "HERO_CREST" Promotion
        item 0x65 "KNIGHT_CREST" Promotion
        item 0x66 "ORION_BOLT" Promotion
        item 0x67 "ELYSIAN_WHIP" Promotion
        item 0x68 "GUIDING_RING" Promotion
        item 0x88 "MASTER_SEAL" MasterSeal
        item 0x97 "CONQUORER_PROOF" Promotion
        item 0x98 "MOON_BRACELET" Other
        item 0x99 "SUN_BRACELET" Other
        item 0x8a "HEAVEN_SEAL" Other
        item 0x69 "CHEST_KEY" Other
        item 0x6a "DOOR_KEY" Other
        item 0x6b "LOCKPICK" Other
        item 0x79 "CHEST_KEY_5" Other
        item 0x6c "VULNERARY" Consumable
        item 0x6d "ELIXIR" Consumable
        item 0x6e "PURE_WATER" Consumable
        item 0x6f "ANTITOXIN" Consumable
        item 0x70 "TORCH" Other
        item 0x71 "FILI_SHIELD" Other
        item 0x72 "MEMBER_CARD" Other
        item 0x73 "SILVER_CARD" Other
        item 0x7c "HOPLON_GUARD" Other
        item 0x89 "METIS_TOME" StatBooster
        item 0x74 "WHITE_GEM" Other
        item 0x75 "BLUE_GEM" Other
        item 0x76 "RED_GEM" Other
        item 0xba "BLACK_GEM" Other
        item 0xbb "GOLD_GEM" Other
        item 0x77 "GOLD_1" Other
        item 0x9a "GOLD_1_AGAIN" Other
        item 0x9b "GOLD_5" Other
        item 0x9c "GOLD_10" Other
        item 0x9d "GOLD_50" Other
        item 0x9e "GOLD_100" Other
        item 0x9f "GOLD_3000" Other
        item 0xa0 "GOLD_5000" Other
        item 0xa "UNUSED_MANI_KATTI" Other
        item 0x3d "UNUSED_FORBLAZE" Other
    ]

    let CHES offset item = reward Chest (offset + 4) item 0
    let ITGV offset item = reward Village (offset + 4) item 0
    let Unit offset item unit pos = reward StartingInventory (offset + 12 + pos) item unit
    let Chap offset item = reward Story (offset + 12) item 0
    let Sand offset item = reward Sand (offset + 12) item 0

    let US = [
        // Fire Emblem - The Sacred Stones (USA, Australia)

        // Prologue: The Fall of Renais

        // Chapter 1: Escape!

        // Chapter 2: The Protected
        ITGV 0x9F0734 0x6E // PURE_WATER
        ITGV 0x9F06BC 0x76 // RED_GEM
        ITGV 0x9F06F8 0x6D // ELIXIR

        // Chapter 3: The Bandits of Borgo
        CHES 0x9E89B4 0x14 // IRON_LANCE
        CHES 0x9E89C0 0x28 // HAND_AXE
        CHES 0x9E89CC 0x01 // IRON_SWORD
        CHES 0x9E89D8 0x1C // JAVELIN

        // Chapter 4: Ancient Horrors
        ITGV 0x9F1C00 0x1F // IRON_AXE

        // Chapter 5: The Empire's Reach
        ITGV 0x9F21E4 0x60 // DRAGONSHIELD
        ITGV 0x9F2220 0x5D // SECRET_BOOK
        ITGV 0x9F2258 0x70 // TORCH
        ITGV 0x9F2198 0x0E // ARMORSLAYER
        Chap 0x9F2134 0x68 // GUIDING_RING

        // Chatper 5x: Unbroken Heart
        CHES 0x9E8CC4 0x6D // ELIXIR
        CHES 0x9E8CD0 0x1A // KILLER_LANCE

        // Chapter 6: Victims of War
        ITGV 0x9F2AC0 0x6F // ANTITOXIN
        Chap 0x9F2A48 0x66 // ORION_BOLT

        // Chapter 7: Waterside Renvall
        Unit 0x8B7030 0x5C 0x80 1 // ENERGY_RING
        Unit 0x8B7094 0x65 0x4C 2 // KNIGHT_CREST

        // Chapter 8: It's a Trap!
        CHES 0x9E8F84 0x04 // SILVER_SWORD
        CHES 0x9E8F90 0x67 // ELYSIAN_WHIP
        CHES 0x9E8FA8 0x5B // ANGELIC_ROBE

        yield! exclusiveTo [Eirika] [
            // Chapter 9 (Eirika): Distant Blade
            ITGV 0x9F3FC8 0x60 // DRAGONSHIELD
            ITGV 0x9F4004 0x09 // RAPIER
            Unit 0x8B7EA0 0x97 0x42 1 // CONQUORER_PROOF
            Unit 0x8B80F8 0x5E 0x12 1 // SPEEDWINGS
            Chap 0x9F3F28 0x5B // ANGELIC_ROBE

            // Chapter 10 (Eirika): Revolt at Carcino
            Unit 0x8B89A0 0x64 0x14 2 // HERO_CREST
            Unit 0x8B8644 0x68 0x4F 1 // GUIDING_RING
            Unit 0x8B89B4 0x5F 0x15 2 // GODDESS_ICON
            Unit 0x8B8A68 0x61 0xA8 1 // TALISMAN

            // Chapter 11 (Eirika): Creeping Darkness
            CHES 0x9E9358 0x50 // RESTORE
            CHES 0x9E9364 0x96 // SHORT_SPEAR
            CHES 0x9E9370 0x5D // SECRET_BOOK

            // Chapter 12 (Eirika): Village of Silence
            ITGV 0x9F5394 0x59 // BARRIER
            Unit 0x8BA5D0 0x5C 0x18 01 // ENERGY_RING

            // Chapter 13 (Eirika): Hamill Canyon
            Unit 0x8BAA4C 0x65 0x51 3 // KNIGHT_CREST
            Unit 0x8BAED4 0x67 0x0E 2 // ELYSIAN_WHIP
            Unit 0x8BAF10 0x5E 0x12 2 // SPEEDWINGS
            Unit 0x8BACE0 0x63 0xA9 1 // BODY_RING

            // Chapter 14 (Eirika): Queen of White Dunes
            Unit 0x8BBE78 0x64 0x80 1 // HERO_CREST
            CHES 0x9E9708 0x57 // HAMMERNE
            CHES 0x9E9714 0x8D // DRAGONSPEAR
            CHES 0x9E9720 0x1D // SPEAR
            CHES 0x9E9738 0x5C // ENERGY_RING
            CHES 0x9E9744 0x68 // GUIDING_RING
            yield! Reward.mututallyExclusive [
                Chap 0x9F5FDC 0x91 // AUDHULMA
                Chap 0x9F6024 0x91 // AUDHULMA
            ]
            yield! Reward.mututallyExclusive [
                Chap 0x9F5FF0 0x3E // EXCALIBUR
                Chap 0x9F6038 0x3E // EXCALIBUR
            ]

            // Chapter 15 (Eirika): Scorched Sand
            Unit 0x8BC8A4 0x68 0x80 2 // GUIDING_RING
            ITGV 0x9F65F0 0x88 // MASTER_SEAL
            Sand 0x9F6868 0x89 // METIS_TOME
            Sand 0x9F68AC 0x54 // WARP
            Sand 0x9F6978 0x73 // SILVER_CARD
            Sand 0x9F6934 0x63 // BODY_RING
            Sand 0x9F67E0 0x0F // WYRMSLAYER
            Sand 0x9F69BC 0x31 // KILLER_BOW
            Sand 0x9F6824 0x62 // BOOTS
            Sand 0x9F68F0 0x48 // ECLIPSE
            Sand 0x9F6A00 0x51 // SILENCE
            Chap 0x9F64B8 0x4A // GLEIPNIR
            Chap 0x9F64CC 0x93 // GARM

            // Chapter 16 (Eirika): Ruled by Madness
            Unit 0x8BD088 0x64 0x8D 1 // HERO_CREST
            CHES 0x9E9A94 0x65 // KNIGHT_CREST
            CHES 0x9E9AA0 0x29 // TOMAHAWK
            CHES 0x9E9AD0 0x61 // TALISMAN

            // Chapter 17 (Eirika): River of Regrets
            Unit 0x8BE038 0x68 0x8D 1 // GUIDING_RING
            Unit 0x8BE13C 0x60 0x8D 1 // DRAGONSHIELD

            // Chapter 18 (Eirika): Two Faces of Evil

            // Chapter 19 (Eirika): Last Hope
            CHES 0x9E9E78 0x11 // RUNE_SWORD
            CHES 0x9E9E84 0x49 // FENRIR
            CHES 0x9E9E90 0x4F // FORTIFY
            CHES 0x9E9EC0 0x5E // SPEEDWINGS
            CHES 0x9E9ECC 0x3B // BOLTING
            Unit 0x8C0470 0x5F 0x8D 1 // GODDESS_ICON

            // Chapter 20 (Eirika): Darkling Woods

            // Final (Eirika): Sacred Stone (Part 1)
            CHES 0x9EA0C0 0x5B // ANGELIC_ROBE
            CHES 0x9EA0CC 0x88 // MASTER_SEAL

            // Final (Eirika): Sacred Stone (Part 2)
        ]

        yield! exclusiveTo [Ephraim] [
            // Chapter 9 (Ephraim): Fort Rigwald
            CHES 0x9EA2A0 0x50 // RESTORE
            CHES 0x9EA2E8 0x97 // CONQUORER_PROOF
            Unit 0x8C26B0 0x5E 0x12 1 // SPEEDWINGS

            // Chapter 10 (Ephraim): Turning Traitor
            Unit 0x8C2EE8 0x5D 0x80 1 // SECRET_BOOK
            Unit 0x8C3050 0x67 0x0E 2 // ELYSIAN_WHIP
            ITGV 0x9FAF7C 0x56 // TORCH_STAFF
            ITGV 0x9FAFB8 0x64 // HERO_CREST
            Chap 0x9FAD7C 0x65 // KNIGHT_CREST

            // Chapter 11 (Ephraim): Phantom Ship

            // Chapter 12 (Ephraim): Landing at Taizel
            Unit 0x8C4938 0x68 0x80 1 // GUIDING_RING
            Unit 0x8C4AB4 0x60 0x18 1 // DRAGONSHIELD

            // Chapter 13 (Ephraim): Fluorspar's Oath
            Unit 0x8C4FF4 0x5C 0x80 2 // ENERGY_RING
            Unit 0x8C524C 0x5E 0x80 1 // SPEEDWINGS
            Unit 0x8C5288 0x5F 0x15 0 // GODDESS_ICON
            Unit 0x8C529C 0x64 0x14 1 // HERO_CREST
            ITGV 0x9FBDB8 0x59 // BARRIER
            ITGV 0x9FBDF4 0x61 // TALISMAN

            // Chapter 14 (Ephraim): Father and Son
            Unit 0x8C60C0 0x65 0x6B 1 // KNIGHT_CREST
            CHES 0x9EA940 0x68 // GUIDING_RING
            CHES 0x9EA9DC 0x57 // HAMMERNE
            CHES 0x9EA9F4 0x1D // SPEAR
            CHES 0x9EAA00 0x5B // ANGELIC_ROBE
            Unit 0x8C62F0 0x63 0x80 1 // BODY_RING
            Chap 0x9FC48C 0x4A // GLEIPNIR
            Chap 0x9FC4A0 0x93 // GARM

            // Chapter 15 (Ephraim): Scorched Sand
            Unit 0x8C70F8 0x68 0x80 2 // GUIDING_RING
            ITGV 0x9FC92C 0x88 // MASTER_SEAL
            Sand 0x9FCB74 0x89 // METIS_TOME
            Sand 0x9FCBB8 0x54 // WARP
            Sand 0x9FCC84 0x73 // SILVER_CARD
            Sand 0x9FCC40 0x63 // BODY_RING
            Sand 0x9FCAEC 0x0F // WYRMSLAYER
            Sand 0x9FCCC8 0x31 // KILLER_BOW
            Sand 0x9FCB30 0x62 // BOOTS
            Sand 0x9FCBFC 0x48 // ECLIPSE
            Sand 0x9FCD0C 0x51 // SILENCE
            Chap 0x9F64F4 0x91 // AUDHULMA
            Chap 0x9F6508 0x3E // EXCALIBUR

            // Chapter 16 (Ephraim): Ruled by Madness
            Unit 0x8C779C 0x64 0x8F 1 // HERO_CREST
            CHES 0x9EAD84 0x29 // TOMAHAWK
            CHES 0x9EAD90 0x61 // TALISMAN
            CHES 0x9EAD9C 0x65 // KNIGHT_CREST

            // Chapter 17 (Ephraim): River of Regrets
            Unit 0x8C7E7C 0x68 0x8D 1 // GUIDING_RING
            Unit 0x8C7F80 0x60 0x8D 1 // DRAGONSHIELD

            // Chapter 18 (Ephraim): Two Faces of Evil

            // Chapter 19 (Ephraim): Last Hope
            CHES 0x9EB18C 0x11 // RUNE_SWORD
            CHES 0x9EB198 0x49 // FENRIR
            CHES 0x9EB1A4 0x4F // FORTIFY
            CHES 0x9EB1D4 0x5E // SPEEDWINGS
            CHES 0x9EB1E0 0x3B // BOLTING
            Unit 0x8C9E90 0x5F 0x8D 1 // GODDESS_ICON

            // Chapter 20 (Ephraim): Darkling Woods

            // Final (Ephraim): Sacred Stone (Part 1)
            CHES 0x9EB3D4 0x5B // ANGELIC_ROBE
            CHES 0x9EB3E0 0x88 // MASTER_SEAL

            // Final (Ephraim): Sacred Stone (Part 2)
        ]

        // Extra end-of-chapter items

        // Chapter 16
        Chap 0x9F71E4 0x92 // SIEGMUND
        Chap 0x9F7204 0x85 // SIEGLINDE

        // Chpater 17
        Chap 0x9F7C0C 0x94 // NIDHOGG
        Chap 0x9F7C20 0x8E // VIDOFNIR
        Chap 0x9F7C90 0x55 // RESCUE

        // Chatper 19
        Chap 0x9F885C 0x87 // IVALDI
        Chap 0x9F8870 0x8C // LATONA
        Chap 0x9F8920 0x10 // LIGHT_BRAND
    ]
