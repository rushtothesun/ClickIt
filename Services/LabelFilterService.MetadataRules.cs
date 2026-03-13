using ClickIt.Utils;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ItemFilterLibrary;

namespace ClickIt.Services
{
    public partial class LabelFilterService
    {
        private static bool ShouldAllowWorldItemByIFL(Entity item, List<ItemFilter> itemFilters, GameController? gameController)
        {
            if (itemFilters == null || itemFilters.Count == 0 || gameController == null)
                return false;

            try
            {
                WorldItem? worldItemComp = item.GetComponent<WorldItem>();
                Entity? innerItemEntity = worldItemComp?.ItemEntity;
                if (innerItemEntity == null)
                    return false;

                var itemData = new ItemData(innerItemEntity, item, gameController);

                for (int i = 0; i < itemFilters.Count; i++)
                {
                    if (itemFilters[i].Matches(itemData))
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the metadata path or item name matches any of the provided metadata identifiers.
        /// Used by strongbox filtering logic.
        /// </summary>
        private static bool ContainsAnyMetadataIdentifier(string metadataPath, string itemName, IReadOnlyList<string> identifiers)
        {
            if (identifiers == null || identifiers.Count == 0)
                return false;

            metadataPath ??= string.Empty;
            itemName ??= string.Empty;

            for (int i = 0; i < identifiers.Count; i++)
            {
                string identifier = identifiers[i] ?? string.Empty;
                if (identifier.Length == 0)
                    continue;

                if (MetadataIdentifierMatcher.ContainsSingle(metadataPath, itemName, identifier))
                    return true;
            }

            return false;
        }
    }
}