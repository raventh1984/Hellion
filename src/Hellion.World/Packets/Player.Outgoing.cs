﻿using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using Hellion.World.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.World.Systems
{
    public partial class Player
    {
        internal void SendPlayerSpawn()
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.WeatherAll, 0x0000FF00);
                packet.Write(0); // Get weather by season


                packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.WorldInformation);
                packet.Write(this.MapId);
                packet.Write(this.Position.X);
                packet.Write(this.Position.Y);
                packet.Write(this.Position.Z);

                packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.ObjectSpawn);

                // Object properties
                packet.Write((byte)this.Type);
                packet.Write(this.ModelId);
                packet.Write((byte)this.Type);
                packet.Write(this.ModelId);
                packet.Write(this.Size);
                packet.Write(this.Position.X);
                packet.Write(this.Position.Y);
                packet.Write(this.Position.Z);
                packet.Write((short)(this.Angle * 10f));
                packet.Write(this.ObjectId);

                packet.Write<short>(0);
                packet.Write<byte>(1); // is player ?
                packet.Write(this.Hp);
                packet.Write(0);
                packet.Write(0);
                packet.Write<byte>(1);

                // baby buffer
                packet.Write(-1);

                packet.Write(this.Name);
                packet.Write(this.Gender);
                packet.Write((byte)this.SkinSetId);
                packet.Write((byte)this.HairId);
                packet.Write((int)this.HairColor);
                packet.Write((byte)this.FaceId);
                packet.Write(this.Id);
                packet.Write((byte)this.ClassId);
                packet.Write((short)this.Strength);
                packet.Write((short)this.Stamina);
                packet.Write((short)this.Dexterity);
                packet.Write((short)this.Intelligence);
                packet.Write((short)this.Level);
                packet.Write(-1); // Fuel
                packet.Write(0); // Actuel fuel

                // Guilds

                packet.Write<byte>(0); // have guild or not
                packet.Write(0); // guild cloak

                // Party

                packet.Write<byte>(0); // have party or not

                packet.Write((byte)this.Authority);
                packet.Write(0); // mode
                packet.Write(0); // state mode
                packet.Write(0x000001F6); // item used ??
                packet.Write(0); // last pk time.
                packet.Write(0); // karma
                packet.Write(0); // pk propensity
                packet.Write(0); // pk exp
                packet.Write(0); // fame
                packet.Write<byte>(0); // duel
                packet.Write(-1); // titles

                for (int i = 0; i < 31; i++)
                    packet.Write(0);

                packet.Write(0); // guild war state

                for (int i = 0; i < 26; ++i)
                    packet.Write(0);

                packet.Write((short)this.Mp);
                packet.Write((short)this.Fp);
                packet.Write(0); // tutorial state
                packet.Write(0); // fly experience
                packet.Write(this.Gold);
                packet.Write(this.Experience);
                packet.Write(0); // skill level
                packet.Write(0); // skill points
                packet.Write<long>(0); // death exp
                packet.Write(0); // death level

                for (int i = 0; i < 32; ++i)
                    packet.Write(0); // job in each level

                packet.Write(0); // marking world id
                packet.Write(this.Position.X);
                packet.Write(this.Position.Y);
                packet.Write(this.Position.Z);

                // Quests
                packet.Write<byte>(0);
                packet.Write<byte>(0);
                packet.Write<byte>(0);

                packet.Write(42); // murderer id
                packet.Write<short>(0); // stat points
                packet.Write<short>(0); // always 0

                // item mask
                for (int i = 0; i < 31; i++)
                    packet.Write(0);

                // skills
                for (int i = 0; i < 45; ++i)
                {
                    packet.Write(0); // skill id
                    packet.Write(0); // skill level
                }

                packet.Write<byte>(41); // cheer point
                packet.Write(42); // next cheer point ?

                // Bank
                packet.Write((byte)this.Slot);
                for (int i = 0; i < 3; ++i)
                    packet.Write(0); // gold
                for (int i = 0; i < 3; ++i)
                    packet.Write(0); // player bank ?

                packet.Write(0x00000001); //ar << m_nPlusMaxHitPoint;
                packet.Write<byte>(0);  //ar << m_nAttackResistLeft;				
                packet.Write<byte>(0);  //ar << m_nAttackResistRight;				
                packet.Write<byte>(0);  //ar << m_nDefenseResist;
                packet.Write<long>(0x00000002); //ar << m_nAngelExp;
                packet.Write(42); //ar << m_nAngelLevel;

                // Inventory

                for (int i = 0; i < 73; ++i)
                    packet.Write(i);

                packet.Write<byte>(0); // item count

                for (int i = 0; i < 73; ++i)
                    packet.Write(i);

                // Bank

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 0x2A; ++j)
                        packet.Write(j);
                    packet.Write<byte>(0); // count
                    for (int j = 0; j < 0x2A; ++j)
                        packet.Write(j);
                }

                packet.Write(int.MaxValue); // pet id

                // Bag

                packet.Write<byte>(1);
                for (int i = 0; i < 6; i++)
                {
                    packet.Write(i);
                }
                packet.Write<byte>(0);                 // Base bag item count
                for (int i = 0; i < 0; i++)
                {
                    packet.Write((byte)i);             // Slot
                    packet.Write(i);            // Slot
                }
                for (int i = 0; i < 6; i++)
                {
                    packet.Write(i);
                }
                packet.Write(0);
                packet.Write(0);

                // premium bag
                packet.Write<byte>(0);

                packet.Write(0); // muted

                // Honor titles
                for (Int32 i = 0; i < 150; ++i)
                    packet.Write(0);

                packet.Write(0); // id campus
                packet.Write(0); // campus points

                // buffs
                packet.Write(0); // count

                // Game time packet
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameTime);
                //packet.Write<short>(0);
                //packet.Write<byte>(0);
                //packet.Write<byte>(0);
                //packet.Write<float>(0);


                //// Wheater
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.WeatherClear);
                //packet.Write(1);
                //packet.Write(0);

                //// party
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.PartyDefaultName);
                //packet.Write(0);

                //// total play time
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.WriteGameJoin);
                //packet.Write(0);  //total play time

                //// server settings
                //packet.StartNewMergedPacket(-1, WorldHeaders.Outgoing.ServerSettings);
                //packet.Write<byte>(0x00);
                //packet.Write<byte>(0x01);  // duel enabled ?
                //packet.Write<byte>(0x01);  // Enable guild warehouse
                //packet.Write<byte>(0x01);  // Enable guild war
                //packet.Write<byte>(0); //Configuration.ClientSettings.EnabledFriendsList ? 0 : 1);  // school
                //packet.Write<byte>(0x00);  // school_battle
                //packet.Write<byte>(0x00); //no flymonster
                //packet.Write<byte>(0x00);  //no darkon
                //packet.Write<byte>(0x00);  //no guild
                //packet.Write<byte>(0x01);  // no wormon
                //packet.Write<byte>(0x00);  // despawn
                //packet.Write<byte>(0x01);  // PK
                //packet.Write<byte>(0x01);  // PKcost
                //packet.Write<byte>(0x00); //steal
                //packet.Write<byte>(0x00); //event0913
                //packet.Write<byte>(0x01);  // guildcombat
                //packet.Write<byte>(0x00); //dropitemremove
                //packet.Write<byte>(0x00); //event1206
                //packet.Write<byte>(0x00); //event1219
                //packet.Write<byte>(0x00); //event0127
                //packet.Write<byte>(0x00); //event0214
                //packet.Write<byte>(0x01);
                //packet.Write<byte>(0x01);  // combat war 1vs 1
                //packet.Write<byte>(0x01);  //arene
                //packet.Write<byte>(0x01);  //secret room
                //packet.Write<byte>(0x01);  //rainbow race

                //for (int i = 0; i < 998; ++i)
                //    packet.Write<byte>(0);

                //#region unknow end packet
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.Unknow3E);
                //packet.Write<Int16>(0);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameRates);
                //packet.Write<Single>(0f);//Configuration.Rates.ShopCostRate);
                //packet.Write<byte>(0);//(byte)RateType.ShopCost);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameRates);
                //packet.Write<Single>(0f);//Configuration.Rates.ExpRate);
                //packet.Write<byte>(0);//(byte)RateType.MonsterExpRate);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameRates);
                //packet.Write<Single>(0f);//Configuration.Rates.DropRate);
                //packet.Write<byte>(0);//(byte)RateType.ItemDropRate);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameRates);
                //packet.Write<Single>(0f);//Configuration.Rates.GoldRate);
                //packet.Write<byte>(0);//(byte)RateType.GoldDropRate);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameRates);
                //packet.Write<Single>(0f);//Configuration.Rates.MonsterHitRate);
                //packet.Write<byte>(0);//(byte)RateType.MonsterHitRate);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GameRates);
                //packet.Write<Single>(0f);//Configuration.Rates.MonsterHPRate);
                //packet.Write<byte>(0);//(byte)RateType.MonsterHitpoint);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.MonsterProp);
                //packet.Write<Int64>(0);
                //#region new unknow from v14
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.AllAction);
                //packet.Write<Int64>(1);
                //#region GUILD COMBAT INFOS
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GuildCombat);
                //packet.Write<byte>(0x08);
                //packet.Write(0);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GuildCombat);
                //packet.Write<byte>(0x30);
                //packet.Write(1);
                //packet.Write(0x69);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GuildCombat);
                //packet.Write<byte>(0x31);
                //packet.Write(Int32.MaxValue);//0xEDDE880F);
                //packet.Write(0x00);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GuildCombat);
                //packet.Write<byte>(0);
                //packet.Write(1);
                //packet.Write(0);
                //packet.Write(0);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GuildCombat);
                //packet.Write<byte>(7);
                //packet.Write(1);
                //#endregion
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.SECRETROOM_MNG_STATE);
                //packet.Write<Int64>(1);
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.TaxAllInformations);
                //packet.Write(3);
                //packet.Write<byte>(0x0F);
                //packet.Write(-1);
                //packet.Write(3);
                //packet.Write<byte>(0);
                //packet.Write(0);
                //packet.Write<byte>(1);
                //packet.Write(0);
                //packet.Write<byte>(2);
                //packet.Write(0);
                //packet.Write<byte>(0x0F);
                //packet.Write(-1);
                //packet.Write(2);
                //packet.Write<byte>(0);
                //packet.Write(0);
                //packet.Write<byte>(1);
                //packet.Write(0);
                //packet.Write<byte>(0xFF);
                //packet.Write(-1);
                //packet.Write(3);
                //packet.Write<byte>(0);
                //packet.Write(0);
                //packet.Write<byte>(1);
                //packet.Write(0);
                //packet.Write<byte>(2);
                //packet.Write(0);
                //#region Maisons de guildes
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.GuildHouseAllInformations);
                //packet.Write(1); //bSetFurnitureChannel
                //packet.Write(0); //bHaveGuildHouse
                //                        /*
                //                        {
                //                            packet.Write(0); //m_dwGuildId
                //                            packet.Write(0); //worldID
                //                            packet.Write(0); //m_tUpkeepTime
                //                            packet.Write(0); //nSize
                //                        }*/
                //#endregion
                //#region Maisons
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.HousingAllInformations);
                //packet.Write(0); //size infos housing
                //packet.Write(0); //size info ami autorisé
                //#endregion

                //#endregion
                //#endregion

                // Message hud
                //packet.StartNewMergedPacket(this.ObjectId, WorldHeaders.Outgoing.MessageHud);
                //packet.Write("Welcome to Hellion Emulator!");

                this.Send(packet);
            }
        }

        internal void SendPlayerSpawn(Player worldObject)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(worldObject.ObjectId, WorldHeaders.Outgoing.ObjectSpawn);

                packet.Write((byte)worldObject.Type);
                packet.Write(worldObject.ModelId);
                packet.Write((byte)worldObject.Type);
                packet.Write(worldObject.ModelId);
                packet.Write(worldObject.Size);
                packet.Write(worldObject.Position.X);
                packet.Write(worldObject.Position.Y);
                packet.Write(worldObject.Position.Z);
                packet.Write((short)(worldObject.Angle * 10f));
                packet.Write(worldObject.ObjectId);

                packet.Write<short>(0);
                packet.Write<byte>(1); // is player?
                packet.Write(worldObject.Hp);
                packet.Write(0); // moving flags
                packet.Write(0); // motion flags
                packet.Write<byte>(0);
                packet.Write(-1); // baby buffer

                packet.Write(worldObject.Name);
                packet.Write(worldObject.Gender);
                packet.Write((byte)worldObject.SkinSetId);
                packet.Write((byte)worldObject.HairId);
                packet.Write((int)worldObject.HairColor);
                packet.Write((byte)worldObject.FaceId);
                packet.Write(worldObject.Id);
                packet.Write((byte)worldObject.ClassId);
                packet.Write((short)worldObject.Strength);
                packet.Write((short)worldObject.Stamina);
                packet.Write((short)worldObject.Dexterity);
                packet.Write((short)worldObject.Intelligence);
                packet.Write((short)worldObject.Level);

                packet.Write(-1);
                packet.Write(0);

                packet.Write<byte>(0); // has guild
                packet.Write(0); // guild cloak

                packet.Write<byte>(0); // has party

                packet.Write((byte)worldObject.Authority);
                packet.Write(0); // mode
                packet.Write(0); // state mode
                packet.Write(0x000001F6); // item used ??
                packet.Write(0); // last pk time.
                packet.Write(0); // karma
                packet.Write(0); // pk propensity
                packet.Write(0); // pk exp
                packet.Write(0); // fame
                packet.Write<byte>(0); // duel
                packet.Write(-1); // titles
                
                for (int i = 0x2A; i < 0x49; i++)
                {
                    packet.Write(0);
                }
                for (int i = 0; i < 28; i++)
                {
                    packet.Write(0);
                }

                packet.Write<byte>(0);

                packet.Write(-1); // pet ?
                packet.Write(0); // buffs ?

                this.Send(packet);
            }
        }

        internal void SendNpcSpawn(Npc npc)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(npc.ObjectId, WorldHeaders.Outgoing.ObjectSpawn);

                packet.Write((byte)npc.Type);
                packet.Write(npc.ModelId);
                packet.Write((byte)npc.Type);
                packet.Write(npc.ModelId);
                packet.Write(npc.Size);
                packet.Write(npc.Position.X);
                packet.Write(npc.Position.Y);
                packet.Write(npc.Position.Z);
                packet.Write((short)(npc.Angle * 10f));
                packet.Write(npc.ObjectId);

                packet.Write<short>(1);
                packet.Write<byte>(0);
                packet.Write(1); // can be selected
                packet.Write(1);
                packet.Write(0);
                packet.Write<byte>(1);
                packet.Write(-1);
                packet.Write<byte>(0);//packet.Write((byte)npc.Data.HairId);
                packet.Write(0);//packet.Write(npc.Data.HairColor);
                packet.Write<byte>(0);//packet.Write((byte)npc.Data.FaceId);
                packet.Write(npc.Name);
                packet.Write<byte>(0); // item equiped count
                packet.Write<byte>(0);
                packet.Write<byte>(0);
                packet.Write<byte>(0);
                packet.Write(0);
                packet.Write<float>(1);
                packet.Write(0);

                this.Send(packet);
            }
        }

        internal void SendDespawnObject(WorldObject worldObject)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(worldObject.ObjectId, WorldHeaders.Outgoing.ObjectDespawn);

                this.Send(packet);
            }
        }
    }
}