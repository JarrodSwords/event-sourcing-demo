﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly Dictionary<Guid, List<IEvent>> _streams = new();

        #region ICharacterRepository Implementation

        public Character Find(Guid id)
        {
            if (!_streams.ContainsKey(id))
                throw new ArgumentException(
                    $"{nameof(Character)} not found.",
                    nameof(id)
                );

            return new Character(id, _streams[id].ToArray());
        }

        public ICharacterRepository Save(Character character)
        {
            if (_streams.ContainsKey(character.Id))
                _streams[character.Id].AddRange(character.Events);
            else
                _streams.Add(character.Id, character.Events.ToList());

            return this;
        }

        #endregion
    }
}
