using UnityEngine;

public class PGFFactory : MonoBehaviour{

    //Singleton
    public static PGFFactory Instance { get; private set; }

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
    public PGF CreatePGF() {
        //Create the PGF
        var newPGF = Instantiate(pgfPrefab);

        if(newPGF != null)
        {
            newPGF.transform.position = pgfStartTransform.position;
            newPGF.transform.forward = pgfStartTransform.forward;
        }

        PGF pgf = newPGF.GetComponent<PGF>();

<<<<<<< HEAD
        pgf.Data = new PGFData()
        {
            meta = CreateMetaData()
=======
    private PGFProjectileTrajectoryData CreateProjectileTrajectoryData() {
        return new PGFProjectileTrajectoryData(){
            speed = UnityEngine.Random.Range(1.0f, 100.0f), distanceBeforeSpread = UnityEngine.Random.Range(0.0f, 10.0f), spreadAngle = 0.0f
        };
    }

    private PGFMetaData CreateMetaData() {
        return new PGFMetaData()
        {
            name = "NAME PLACEHOLDER", type = "shootyboy"

>>>>>>> 71a16b376305702eabe72150917d8bc52314b2dc
        };

        return pgf;
    }

    private PGFMetaData CreateMetaData() {
        return new PGFMetaData()
        {
            //Any
            name = "TEST w RAND NUM " + RandomUtility.RandFloat(-1000.0f, 1000.0f),
            //Any
            type = "shootyboy"
        };
    }
}
