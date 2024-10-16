namespace FlappyBirdCerto;

public partial class MainPage : ContentPage
{
	const int gravidade = 7;
	const int tempoEntreFrames = 25;
	bool estaMorto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 10;
	const int maxTempoPulando = 3;
	int TempoPulando = 1;
	bool estaPulando = false;
	const int forcaPulo = 40;
	const int aberturaMinima=10;



	public MainPage()
	{
		InitializeComponent();

	}
	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		larguraJanela = width;
		alturaJanela= height;
	}
	void GerenciaCanos()
	{
		canocima.TranslationX -= velocidade;
		canobaixo.TranslationX -= velocidade;
		if (canobaixo.TranslationX < -larguraJanela)
		{
			canobaixo.TranslationX = 0;
			canocima.TranslationX = 0;
			var alturaMax=-50;
			var alturaMin=-canobaixo.HeightRequest;
			canocima.TranslationY=Random.Shared.Next((int)alturaMin, (int)alturaMax);
			canobaixo.TranslationY=canocima.TranslationY+aberturaMinima+canobaixo.HeightRequest;
		}
	}

	void AplicaGravidade()
	{
		bird.TranslationY += gravidade;
	}
	async Task Desenhar()
	{
		while (!estaMorto)
		{
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
			GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				FrameGameOver.IsVisible = true;
				break;
			}

			await Task.Delay(tempoEntreFrames);
		}
	}
	void AplicaPulo()
	{
		bird.TranslationY -= forcaPulo;
		TempoPulando++;
		if (TempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			TempoPulando = 0;
		}
	}

	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			if (VerificaColisaoTeto() ||
			   VerificaColisaoChao())
			{
				return true;
			}
		}
		return false;
	}

	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (bird.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2 - grama.HeightRequest;
		if (bird.TranslationY >= maxY)
			return true;
		else
			return false;
	}
	void OnGameOverClicked(object sender, TappedEventArgs args)
	{
		FrameGameOver.IsVisible = false;
		Inicializar();
		Desenhar();
	}
	void Inicializar()
	{
		estaMorto = false;
		bird.TranslationY = 0;
	}
	void OnGridClicked(object sender, TappedEventArgs args)
	{
		estaPulando = true;
	}
}


