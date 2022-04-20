
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour , IcraftingItem
{
    public static int MetalCount=5;
    public static int FleshCount=4;
    public static int GunPowderCount=6;
    public static int ClothCount=8;
    Text MetalTxt ;
    Text FleshTxt;
    Text GunPowderTxt;
    Text ClothTxt;

    public static int getMetalCount()
    {
        return MetalCount;
    }
    public static void setMetalCount(int metalCnt)
    {
        MetalCount = metalCnt;
    }

    public static int getFleshCount()
    {
        return FleshCount;
    }
    public static void setFleshCount(int fleshCnt)
    {
        FleshCount = fleshCnt;
    }

    public static int getGunPowderCount()
    {
        return GunPowderCount;
    }
    public static void setGunPowderCount(int gunpowderCnt)
    {
        GunPowderCount = gunpowderCnt;
    }
    public static int getClothCount()
    {
        return ClothCount;
    }
    public static void setClothCount(int clothCnt)
    {
        ClothCount = clothCnt;
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateResources();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResources();
    }

    public void UpdateResources()
    {
        Transform MetalTransform = transform.Find("Metal").GetChild(0).GetChild(1);
        MetalTxt = MetalTransform.GetComponent<Text>();
        MetalTxt.text = MetalCount.ToString();

        Transform FleshTransform = transform.Find("Flesh").GetChild(0).GetChild(1);
        FleshTxt = FleshTransform.GetComponent<Text>();
        FleshTxt.text = FleshCount.ToString();

        Transform GunPowderTransform = transform.Find("GunPowder").GetChild(0).GetChild(1);
        GunPowderTxt = GunPowderTransform.GetComponent<Text>();
        GunPowderTxt.text = GunPowderCount.ToString();

        Transform ClothTransform = transform.Find("Cloth").GetChild(0).GetChild(1);
        ClothTxt = ClothTransform.GetComponent<Text>();
        ClothTxt.text = ClothCount.ToString();

    }

    public int itemCount(string itemName, InventoryItemBase item)
    {
        if (itemName == "flesh")
        {
            return getFleshCount();
        }

        if (itemName == "cloth")
        {
            return getClothCount();
        }

        if (itemName == "metal")
        {
            return getMetalCount();
        }

        if (itemName == "gunpowder")
        {
            return getGunPowderCount();
        }

        return 0;
    }

    public bool ConsumeItem(string itemName , InventoryItemBase item)
    {

        bool isEnoughResource = false;
        if (itemName == "flesh")
        {
            if (FleshCount > 0)
            {
                setFleshCount(FleshCount--);
                FleshTxt.text = FleshCount.ToString();
                isEnoughResource = true;
            }
           
        }

        if (itemName == "cloth")
        {
            if(ClothCount > 0)
            {
                setFleshCount(ClothCount--);
                ClothTxt.text = ClothCount.ToString();
                isEnoughResource = true;

            }

        }

        if (itemName == "metal")
        {
            if (MetalCount > 0)
            {
                setFleshCount(MetalCount--);
                MetalTxt.text = MetalCount.ToString();
                isEnoughResource = true;

            }
        }

        if (itemName == "gunpowder")
        {
            if (GunPowderCount > 0)
            {
                setFleshCount(GunPowderCount--);
                GunPowderTxt.text = GunPowderCount.ToString();
                isEnoughResource = true;

            }
        }
        return isEnoughResource;

    }

    public void ProduceItem(string itemName, InventoryItemBase item)
    {
        throw new System.NotImplementedException();
    }
}
