using UnityEngine;
using Endless_it1;

public class ObjectStore : MonoBehaviour{

    public SoundManager sm;
    public MusicManager mm;
    public VoiceController vc;
    public InputHandler ih;
    public GameFlowController gfc;
    public ImageManager im;
    public ConnectorManager cmService;
    public ConnectorManager cmStory;
    public DialogueManager dm;
    public InputResponder ir;
    public GameManager gm;
    public GameScreenController gsc;
    public ImageAnimations ia;
    public LoadingUI loadUI;
    public MusicFinder mf;
    public MusicTracker mt;
    public ErrorHandler eh;
    public ImageStore iStore;
    public SoundStore ss;
    public IngameErrorsAPI ie;
    public DamageAnimations da;
    public StartingSceneSelectables sss;
    public MinigameSelectables mg;
    public Options o;
    public MessageUI messageUI;

    void Awake(){
        sss = FindObjectOfType<StartingSceneSelectables>();
        sm = FindObjectOfType<SoundManager>();
        mm = FindObjectOfType<MusicManager>();
        vc = FindObjectOfType<VoiceController>();
        ih = FindObjectOfType<InputHandler>();
        gfc = FindObjectOfType<GameFlowController>();
        im = FindObjectOfType<ImageManager>();
        ir = FindObjectOfType<InputResponder>();
        gm = FindObjectOfType<GameManager>();
        gsc = FindObjectOfType<GameScreenController>();
        ia = FindObjectOfType<ImageAnimations>();
        loadUI = FindObjectOfType<LoadingUI>();
        mf = FindObjectOfType<MusicFinder>();
        mt = FindObjectOfType<MusicTracker>();
        eh = FindObjectOfType<ErrorHandler>();
        iStore = FindObjectOfType<ImageStore>();
        ss = FindObjectOfType<SoundStore>();
        ie = FindObjectOfType<IngameErrorsAPI>();
        da = FindObjectOfType<DamageAnimations>();
        mg = FindObjectOfType<MinigameSelectables>();
        o = FindObjectOfType<Options>();
        messageUI = FindObjectOfType<MessageUI>();
        cmService = new ConnectorManager(" ", this);
    }

    public void UpdateAfterDm(){
        dm = FindObjectOfType<DialogueManager>();
    }
}