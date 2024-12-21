namespace Subnautica.Client.Synchronizations.InitialSync
{
    using Subnautica.API.Extensions;
    using Subnautica.API.Features;
    using Subnautica.Network.Models.Storage.Story.Components;
    using System.Collections;
    using UWE;

    public class CustomDoorways
    {
        private const string DoorwayClassId = "d9d5c46d-32ab-492f-af43-830d72656dcf";

        public static IEnumerator OnDoorwaysInitialized()
        {
            if (Network.Session.Current != null)
            {
                foreach (var door in Network.Session.Current.Story.CustomDoorways)
                {
                    if (door.IsActive)
                    {
                        yield return SpawnDoorway(door);
                    }
                }
            }
        }

        private static IEnumerator SpawnDoorway(CustomDoorwayComponent door)
        {
            var request = PrefabDatabase.GetPrefabAsync(DoorwayClassId);
            yield return request;

            if (request.TryGetPrefab(out var prefab))
            {
                var gameObject = global::Utils.SpawnFromPrefab(prefab, null);
                if (gameObject)
                {
                    Network.Identifier.SetIdentityId(gameObject, door.UniqueId);

                    gameObject.transform.position = door.Position.ToVector3();
                    gameObject.transform.rotation = door.Rotation.ToQuaternion();
                    gameObject.transform.localScale = door.Scale.ToVector3();

                    yield return CoroutineUtils.waitForNextFrame;

                    if (gameObject.TryGetComponent<PrecursorDoorway>(out var component))
                    {
                        component.EnableField();
                    }
                }
            }
        }
    }
}
