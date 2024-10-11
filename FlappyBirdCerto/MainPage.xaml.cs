namespace FlappyBirdCerto;

public partial class MainPage : ContentPage
{
	const int gravidade=3;
	const int tempoEntreFrames=25;
	bool estaMorto=true;
	double larguraJanela=0;
	double alturaJanela=0;
	int velocidade=20;



	public MainPage()
	{
		InitializeComponent();
		
	}
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		larguraJanela=width;
    }
	void GerenciaCanos ()
	{
		canocima.TranslationX -= velocidade;
		canobaixo.TranslationX -= velocidade;
		if(canobaixo.TranslationX<-larguraJanela)
		{
			canobaixo.TranslationX=0;
			canocima.TranslationX=0;
		}
	}
   
    void AplicaGravidade ()
	{
		bird.TranslationY+= gravidade;
	}
	async Task Desenhar ()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);
			GerenciaCanos();
		}
	}
	void OnGameOverClicked (object sender, TappedEventArgs args)
	{
		FrameGameOver.IsVisible = false;
		Inicializar();
		Desenhar ();
	}
	void Inicializar()
	{
		estaMorto=false;
		bird.TranslationY=0;
	}
}


