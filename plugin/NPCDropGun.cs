using UnityEngine;

namespace Oxide.Plugins
{
    [Info("NPC Drop Gun", "2CHEVSKII", "1.0.4")]
    [Description("Forces NPC to drop used gun and some ammo after death")]
    class NPCDropGun : RustPlugin
    {
        /*TODO:
         * Make configuration*/
        #region [Global variables]
        int ammoType;
        string heldWeapon;
        #endregion
        #region [Oxide hooks]
        void OnEntityDeath(BaseCombatEntity entity, HitInfo info)
        {
            if(info != null && entity != null)
            {
                if(entity is HTNPlayer || entity is Scientist)
                {
                    BasePlayer basePlayer = entity as BasePlayer;
                    if(basePlayer != null)
                        ItemSpawner(basePlayer.GetHeldEntity(), basePlayer.ServerPosition);
                    

                    /*BasePlayer basePlayer = entity as HTNPlayer;
                    BasePlayer anotherBaseplayer = entity as Scientist;
                    if(basePlayer != null && basePlayer.GetHeldEntity() != null)
                    {
                        ItemSpawner(basePlayer.GetHeldEntity(), basePlayer.ServerPosition);
                    }
                    else if(anotherBaseplayer !=null && anotherBaseplayer.GetHeldEntity() != null)
                    {
                        ItemSpawner(anotherBaseplayer.GetHeldEntity(), anotherBaseplayer.ServerPosition);
                    }*/
                }
                /*if(entity is HTNPlayer)
                {
                    HTNPlayer hTNPlayer = entity as HTNPlayer;
                    if(hTNPlayer.GetHeldEntity() != null)
                    {
                        ItemSpawner(hTNPlayer.GetHeldEntity(), hTNPlayer.ServerPosition);
                    }
                }
                else if(entity is Scientist)
                {
                    Scientist scientist = entity as Scientist;
                    if(scientist.GetHeldEntity() != null)
                    {
                        ItemSpawner(scientist.GetHeldEntity(), scientist.ServerPosition);
                    }
                }*/

                //Debug  
                /*BasePlayer player = BasePlayer.FindByID(76561198049067915);
                player.ChatMessage(string.Format("DebugInfo: Entity dead. Name: {0}, Held ent: {1}", entity.ShortPrefabName, heldWeapon));*/
            }
        }
        void OnEntitySpawned(BaseNetworkable entity)
        {
            if(entity is NPCPlayerCorpse)
                MethodThatPutsLootIntoCorpse(entity);
        }
        #endregion
        #region [Helpers]
        void AmmoAssigner(string ammoString)
        {
            switch(ammoString)
            {
                case "lr300.entity":
                    ammoType = -1211166256;
                    break;
                case "ak47u.entity":
                    ammoType = -1211166256;
                    break;
                case "semi_auto_rifle.entity":
                    ammoType = -1211166256;
                    break;
                case "m249.entity":
                    ammoType = -1211166256;
                    break;
                case "spas12.entity":
                    ammoType = -1685290200;
                    break;
                case "m92.entity":
                    ammoType = 785728077;
                    break;
                case "mp5.entity":
                    ammoType = 785728077;
                    break;
                default:
                    ammoType = -1211166256;
                    break;
            }
        }
        void ItemSpawner(BaseEntity heldEntity, Vector3 dropPosition)
        {
            if(heldEntity != null)
            {
                Item weapon = ItemManager.CreateByItemID(heldEntity.GetItem().info.itemid);
                if(weapon !=null)
                {
                    weapon.condition = Random.Range(1f, 100f);
                    ItemContainer container = new ItemContainer();
                    container.Insert(weapon);
                    DropUtil.DropItems(container, dropPosition);
                    heldWeapon = heldEntity.ShortPrefabName;
                    AmmoAssigner(heldWeapon);
                }
            }
        }
        void MethodThatPutsLootIntoCorpse(BaseNetworkable entity)
        {
            if(entity != null)
            {
                Item ammo = ItemManager.CreateByItemID(ammoType, Random.Range(0, 60));
                PlayerCorpse corpse = entity.GetComponent<PlayerCorpse>();
                NextTick(() => {
                    if(ammo != null && corpse != null)
                    {
                        ammo.MoveToContainer(corpse.containers[0]);
                    }});
            }
            
        }
        #endregion
    }
}
