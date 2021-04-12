﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace TomatBot.Core.Content.Configs
{
    public sealed class GuildConfig
    {
        [JsonIgnore]
        public ulong AssociatedId { get; internal set; }

        /// <summary>
        /// (dev notes)
        /// key -> user id
        /// uint1 -> prestiges
        /// uint2 -> levels
        /// uint3 -> experience
        /// </summary>
        public Dictionary<ulong, (uint, uint, uint)> LevelData;

        [JsonConstructor]
        public GuildConfig(ulong id)
        {
            AssociatedId = id;
            LevelData = new Dictionary<ulong, (uint, uint, uint)>();
        }
    }
}
