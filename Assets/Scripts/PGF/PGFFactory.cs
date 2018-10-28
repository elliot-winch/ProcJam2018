using UnityEngine;
using TMPro;
public class PGFFactory : MonoBehaviour{

    //Singleton
    public static PGFFactory Instance { get; private set; }

    public TextMeshProUGUI ammo;
    //Assign singleton
    private void Awake()
    {
        Instance = this;
    }

    [Tooltip("The Prefab for any PGF")]
    public GameObject pgfPrefab;
    public Transform pgfStartTransform;

    /// <summary>
    /// Creates a new PGF
    /// </summary>
    /// <returns></returns>
    public PGF CreatePGF()
    {
        //Create the PGF
        var newPGF = Instantiate(pgfPrefab);

        if (newPGF != null)
        {
            newPGF.transform.position = pgfStartTransform.position;
            newPGF.transform.forward = pgfStartTransform.forward;
            newPGF.transform.rotation = pgfStartTransform.rotation;

        }

        PGF pgf = newPGF.GetComponent<PGF>();
        pgf.ammo = ammo;
        pgf.transform.SetParent(Camera.main.transform);

        pgf.Data = new PGFData()
        {
            meta = CreateMetaData()

        };
        return pgf;
    }
        private PGFMetaData CreateMetaData() {
        return new PGFMetaData()
        {
            name = PGFRandomNames.GenerateName(),
            type = "shootyboy"
        };
    }
}
