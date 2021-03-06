﻿using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using Hellion.World.Structures;

namespace Hellion.World.Systems
{
    public partial class Player
    {
        internal void SendItemMove(byte sourceSlot, byte destinationSlot)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(this.ObjectId, SnapshotType.MOVEITEM);
                packet.Write<byte>(0);
                packet.Write(sourceSlot);
                packet.Write(destinationSlot);

                this.Send(packet);
            }
        }

        internal void SendItemEquip(Item item, int targetSlot, bool equip)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(this.ObjectId, SnapshotType.DOEQUIP);
                packet.Write(item.UniqueId);
                packet.Write<byte>(0);
                packet.Write(equip ? (byte)0x01 : (byte)0x00);
                packet.Write(item.Id);
                packet.Write<short>(0); // Refine
                packet.Write<byte>(0); // element
                packet.Write<byte>(0); // element refine
                packet.Write(0);
                packet.Write(targetSlot);

                this.SendToVisible(packet);
            }
        }

        internal void SendCreateItem(Item item)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(this.ObjectId, SnapshotType.CREATEITEM);
                packet.Write<byte>(0);
                packet.Write(-1);
                item.Serialize(packet);
                packet.Write<byte>(1);
                packet.Write((byte)item.Slot);
                packet.Write((short)item.Quantity);

                this.Send(packet);
            }
        }
    }
}
