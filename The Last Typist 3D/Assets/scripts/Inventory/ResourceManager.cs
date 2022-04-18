
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public int MetalCount;
    public int FleshCount;
    public int GunPowderCount;
    public int ClothCount;
    Text MetalTxt ;
    Text FleshTxt;
    Text GunPowderTxt;
    Text ClothTxt;

    public int getMetalCount()
    {
        return MetalCount;
    }
    public void setMetalCount(int metalCnt)
    {
        MetalCount = metalCnt;
    }

    public int getFleshCount()
    {
        return FleshCount;
    }
    public void setFleshCount(int fleshCnt)
    {
        FleshCount = fleshCnt;
    }

    public int getGunPowderCount()
    {
        return GunPowderCount;
    }
    public void setGunPowderCount(int gunpowderCnt)
    {
        GunPowderCount = gunpowderCnt;
    }
    public int getClothCount()
    {
        return ClothCount;
    }
    public void setClothCount(int clothCnt)
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
    }

    public void UpdateResources()
    {
        Transform MetalTransform = transform.Find("Metal").GetChild(0).GetChild(1);
        MetalTxt = MetalTransform.GetComponent<Text>();
        MetalCount = int.Parse(MetalTxt.text);

        Transform FleshTransform = transform.Find("Flesh").GetChild(0).GetChild(1);
        FleshTxt = FleshTransform.GetComponent<Text>();
        FleshCount = int.Parse(FleshTxt.text);

        Transform GunPowderTransform = transform.Find("GunPowder").GetChild(0).GetChild(1);
        GunPowderTxt = GunPowderTransform.GetComponent<Text>();
        GunPowderCount = int.Parse(GunPowderTxt.text);

        Transform ClothTransform = transform.Find("Cloth").GetChild(0).GetChild(1);
        ClothTxt = ClothTransform.GetComponent<Text>();
        ClothCount = int.Parse(ClothTxt.text);
    }

    public int itemCount(string itemName)
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

    public void ConsumeItem(string itemName)
    {

        if (itemName == "flesh")
        {
            setFleshCount(FleshCount--);
            FleshTxt.text = FleshCount.ToString();
        }

        if (itemName == "cloth")
        {
            setFleshCount(ClothCount--);
            ClothTxt.text = ClothCount.ToString();
        }

        if (itemName == "metal")
        {
            setFleshCount(MetalCount--);
            MetalTxt.text = MetalCount.ToString();
        }

        if (itemName == "gunpowder")
        {
            setFleshCount(GunPowderCount--);
            GunPowderTxt.text = GunPowderCount.ToString();
        }

    }
}
