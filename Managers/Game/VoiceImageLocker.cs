public static class VoiceImageLocker{
    private static bool lockFromVoice = true;
    private static bool lockFromImage = true;


    //gets called when the voice is ready to be played
    //alerts classes if image is ready too
    public static void UnlockFromVoice(ObjectStore os){
        if (!lockFromImage){
            ActivateAndAlert(os);
        }
        lockFromVoice = false;
    }


    //gets called when the image is ready to be displayed
    //alerts classes if voice is ready too
    public static void UnlockFromImage(ObjectStore os){
        if (!lockFromVoice){
            ActivateAndAlert(os);
        }
        lockFromImage = false;
    }


    //alerts classes that both voice and image are ready
    private static void ActivateAndAlert(ObjectStore os){
        os.gsc.VoiceAndImageReadyAlert();
        os.dm.VoiceAndImageReadyAlert();
        if (!os.gfc.conversationMode){
            os.im.AnimateImages();
        }
        os.loadUI.DeactivateLoadingUI();
    }


    public static void resetLocks(){
        lockFromVoice = true;
        lockFromImage = true;
    }
}
