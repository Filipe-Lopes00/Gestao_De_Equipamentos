using System.Collections;

internal class Program
{
   
    static int ContadorDeEquipamento = 1;
    static int ContadorDeChamados = 1;
    static ArrayList listaIdsEquipamentos = new ArrayList();
    static ArrayList listaNomeEquipamentos = new ArrayList();
    static ArrayList listaPrecoEquipamentos = new ArrayList();
    static ArrayList listaNumeroSerieEquipamentos = new ArrayList();
    static ArrayList listaDatasFabricacaoEquipamentos = new ArrayList();
    static ArrayList listaFabricantesEquipamentos = new ArrayList();

    static ArrayList listaIdsChamado = new ArrayList();
    static ArrayList listaIdsEquipamentoChamado = new ArrayList();
    static ArrayList listaTitulosChamado = new ArrayList();
    static ArrayList listaDescricoesChamado = new ArrayList();
    static ArrayList listaDatasAberturaChamado = new ArrayList();

    static void Main(string[] args)
    {
        CadastrarAlgunEquipamentosAutomaticamente();
        CadastrarAlgunsChamadosAutomaticamente();

        while (true)
        {
            string opcao = ApresentarMenuPrincipal();

            if (opcao == "s")
                break;

            if (opcao == "1")
            {
                string opcaoCadastroEquipamento = ApresentarMenuCadastroEquipamento();

                if (opcaoCadastroEquipamento == "s")
                    continue;

                CadastroDeEquipamento(opcaoCadastroEquipamento);

            }
            else if (opcao == "2")
            {
                string opcaoCadastroChamados = ApresentarMenuCadastroChamados();

                if (opcaoCadastroChamados == "s")
                    continue;

                ControleChamados(opcaoCadastroChamados);
            }
        }
    }

    static void ControleChamados(string opcaoCadastroChamados)
    {
        if (opcaoCadastroChamados == "1")
        {
            inserirNovoChamado();
        }
        else if (opcaoCadastroChamados == "2")
        {
            bool temChamados = VisualizarChamado(true);

            if (temChamados)
                Console.ReadLine();
        }
        else if (opcaoCadastroChamados == "3")
        {
            EditarChamado();
        }
        else if (opcaoCadastroChamados == "4")
        {
            ExcluirChamado();
        }
    }

    static void ExcluirChamado()
    {
        MostrarCabecalho("Cadastro de Chamados", "Excluindo Chamado: ");

        bool temChamados = VisualizarChamado(false);

        if (temChamados == false)
            return;

        Console.WriteLine();

        int idSelecioando = EncontrarChamado();

        int posicao = listaIdsChamado.IndexOf(idSelecioando);

        listaIdsChamado.RemoveAt(posicao);
        listaTitulosChamado.RemoveAt(posicao);
        listaDescricoesChamado.RemoveAt(posicao);
        listaIdsEquipamentoChamado.RemoveAt(posicao);
        listaDatasAberturaChamado.RemoveAt(posicao);

        ApresentarMensagem("Chamado excluído com sucesso!!!", ConsoleColor.Green);
    }

    static void EditarChamado()
    {
        MostrarCabecalho("Controle de chamados", "Editando chamado: ");

        bool temChamados = VisualizarChamado(false);

        if (temChamados == false)
            return;

        Console.WriteLine();

        int idSelecionado = EncontrarChamado();

        GravarChamado(idSelecionado, "EDITAR");

        ApresentarMensagem("Chamado editado com sucesso!", ConsoleColor.Green);

    }

    static int EncontrarChamado()
    {
        int idSelecionado;
        bool idInvalido;

        do
        {
            Console.WriteLine("Digite o ID do Chamdo: ");

            idSelecionado = Convert.ToInt32(Console.ReadLine());

            idInvalido = listaIdsChamado.Contains(idSelecionado) == false;

            if (idInvalido)
            {
                ApresentarMensagem("ID inválido, tente novamente", ConsoleColor.Red);
                idSelecionado = 0;
            }

        } while (idInvalido);

        return idSelecionado;
    }

    static bool VisualizarChamado(bool mostrarCabecalho)
    {
        if (mostrarCabecalho)
            MostrarCabecalho("Controle de chamados", "Visualizando chamados: ");

        if (listaIdsEquipamentos.Count == 0)
        {
            ApresentarMensagem("Nenhum chamado cadastrado!!", ConsoleColor.DarkYellow);
            return false;

        }
        Console.ForegroundColor = ConsoleColor.Blue;

        Console.WriteLine("{0,-10} | {1,-40} | {2,-30} | {3,-30}", "ID", "Titulo", "Equipamento", "Data de Abertura");

        Console.WriteLine("-------------------------------------------------------------------------------------------------");

        for (int i = 0; i < listaIdsChamado.Count; i++)
        {
            string nomeEquipamento = ObterNomeDoEquipamento((int)listaIdsEquipamentos[i]);

            Console.WriteLine("{0,-10} | {1,-40} | {2,-30} | {3,-30}", listaIdsChamado[i], listaTitulosChamado[i], nomeEquipamento, listaDatasAberturaChamado[i]);
        }

        Console.ResetColor();

        return true;
    }

    static string ObterNomeDoEquipamento(int id)
    {
        int posicao = listaIdsEquipamentos.IndexOf(id);

        string nomeEquipamento = (string)listaNomeEquipamentos[posicao];
        return nomeEquipamento;
    }

    static void inserirNovoChamado()
    {
        MostrarCabecalho("Cadastro de Chamados", "Visualizando Chamados");

        GravarChamado(ContadorDeEquipamento, "INSERIR");

        IncrementarIdChamado();

        ApresentarMensagem("Chamado inserido com sucesso!", ConsoleColor.Green);
    }

    static void IncrementarIdChamado()
    {
        ContadorDeChamados++;
    }

    static void GravarChamado(int id, string tipoOperacao)
    {
        VisualizarEquipamentos(false);

        Console.WriteLine();

        int idEquipamentosChamado = EncontrarEquipamento();

        Console.WriteLine("Digite o titulo do chamado ");
        string titulo = Console.ReadLine();

        Console.WriteLine("Digite a descrição do chamado ");
        string descricao = Console.ReadLine();

        Console.WriteLine("Digite a data de abertura do chamado ");
        string dataAbertura = Console.ReadLine();

        if (tipoOperacao == "INSERIR")
        {
            listaIdsChamado.Add(id);
            listaIdsEquipamentoChamado.Add(idEquipamentosChamado);
            listaTitulosChamado.Add(titulo);
            listaDescricoesChamado.Add(descricao);
            listaDatasAberturaChamado.Add(dataAbertura);

        }

        else if (tipoOperacao == "EDITAR")
        {
            int posicao = listaIdsChamado.IndexOf(id);
            listaIdsChamado[posicao] = id;
            listaIdsEquipamentoChamado[posicao] = (idEquipamentosChamado);
            listaTitulosChamado[posicao] = titulo;
            listaDescricoesChamado[posicao] = descricao;
            listaDatasAberturaChamado[posicao] += descricao;

        }

    }

    static string ApresentarMenuCadastroChamados()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Cadastro de Chamados");
        Console.WriteLine();
        Console.WriteLine("Digite 1 para inserir um novo Chamados");
        Console.WriteLine("Digite 2 para visualizar Chamados cadastrados ");
        Console.WriteLine("Digite 3 para editar Chamados");
        Console.WriteLine("Digite 4 para excluir Chamados");
        Console.WriteLine();
        Console.WriteLine("Digite s para voltar para o menu principal");

        string opcao = Console.ReadLine();

        return opcao;
    }

    static void CadastroDeEquipamento(string opcaoCadastroEquipamento)
    {
        if (opcaoCadastroEquipamento == "1")
        {
            inserirNovoEquipamento();
        }
        else if (opcaoCadastroEquipamento == "2")
        {
            bool temEquipamento = VisualizarEquipamentos(true);

            if (temEquipamento)
                Console.ReadLine();
        }
        else if (opcaoCadastroEquipamento == "3")
        {
            EditarEquipamento();
        }
        else if (opcaoCadastroEquipamento == "4")
        {
            ExcluirEquipamento();
        }
    }

    static void ExcluirEquipamento()
    {
        MostrarCabecalho("Cadastro de Equipamentos", "Excluindo Equipamento: ");

        bool temEquipamentosGravados = VisualizarEquipamentos(false);

        if (temEquipamentosGravados == false)
            return;

        Console.WriteLine();

        int idSelecioando = EncontrarEquipamento();

        int posicao = listaIdsEquipamentos.IndexOf(idSelecioando);

        listaIdsEquipamentos.RemoveAt(posicao);
        listaNomeEquipamentos.RemoveAt(posicao);
        listaPrecoEquipamentos.RemoveAt(posicao);
        listaNumeroSerieEquipamentos.RemoveAt(posicao);
        listaDatasFabricacaoEquipamentos.RemoveAt(posicao);
        listaDatasFabricacaoEquipamentos.RemoveAt(posicao);

        ApresentarMensagem("Equipamento excluído com sucesso!!!", ConsoleColor.Green);

    }

    static void EditarEquipamento()
    {
        MostrarCabecalho("Cadastro de Equipamentos", "Editando Equipamento: ");

        bool temEquipamentos = VisualizarEquipamentos(false);

        if (temEquipamentos == false)
            return;

        Console.WriteLine();

        int idSelecionado = EncontrarEquipamento();

        GravarEquipamento(idSelecionado, "EDITAR");

        ApresentarMensagem("Equipamento editado com sucesso!", ConsoleColor.Green);

    }

    static int EncontrarEquipamento()
    {
        int idSelecionado;
        bool idInvalido;
        do
        {
            Console.WriteLine("Digite o ID do Equipamento: ");

            idSelecionado = Convert.ToInt32(Console.ReadLine());

            idInvalido = listaIdsEquipamentos.Contains(idSelecionado) == false;

            if (idInvalido)
            {
                ApresentarMensagem("ID inválido, tente novamente", ConsoleColor.Red);
                idSelecionado = 0;
            }

        } while (idInvalido);

        return idSelecionado;


    }

    static void GravarEquipamento(int id, string tipoOperacao)
    {
        string nome;
        bool nomeInvalido;
        do
        {
            nomeInvalido = false;
            Console.Write("Digite o nome do Equipamento: ");
            nome = Console.ReadLine();

            if (nome.Length < 6)
            {
                nomeInvalido = true;

                ApresentarMensagem("Nome Inválido. Informe no minimo 6 letras", ConsoleColor.Red);

            }
        }
        while (nomeInvalido);

        Console.Write("Digite o Preço do Equipamento: ");
        decimal preco = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Digite o número de série: ");
        string numeroSerie = Console.ReadLine();

        Console.Write("Digite a data da fabricação: ");
        string dataFabricacao = Console.ReadLine();

        Console.Write("Digite o nome fabricante: ");
        string nomeFabricante = Console.ReadLine();

        if (tipoOperacao == "INSERIR")
        {
            listaIdsEquipamentos.Add(id);
            listaNomeEquipamentos.Add(nome);
            listaPrecoEquipamentos.Add(preco);
            listaNumeroSerieEquipamentos.Add(numeroSerie);
            listaDatasFabricacaoEquipamentos.Add(dataFabricacao);
            listaFabricantesEquipamentos.Add(nomeFabricante);

        }

        else if (tipoOperacao == "EDITAR")
        {
            int posicao = listaIdsEquipamentos.IndexOf(id);

            listaIdsEquipamentos[posicao] = id;
            listaNomeEquipamentos[posicao] = nome;
            listaPrecoEquipamentos[posicao] = preco;
            listaNumeroSerieEquipamentos[posicao] = numeroSerie;
            listaDatasFabricacaoEquipamentos[posicao] = dataFabricacao;
            listaFabricantesEquipamentos[posicao] = nomeFabricante;
        }


    }

    static void ApresentarMensagem(string mensagem, ConsoleColor cor)
    {
        Console.WriteLine();
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
        Console.ReadLine();
    }

    static void MostrarCabecalho(string titulo, string subtitulo)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine(titulo);
        Console.WriteLine();
        Console.WriteLine(subtitulo);
        Console.WriteLine();
        Console.WriteLine();

    }

    static bool VisualizarEquipamentos(bool mostrarCabecalho)
    {
        if (mostrarCabecalho)
            MostrarCabecalho("Cadastro de Equipamentos", "Visualizando Equipamento: ");

        if (listaIdsEquipamentos.Count == 0)
        {
            ApresentarMensagem("Nenhum equipamento cadastrado!!", ConsoleColor.DarkYellow);
            return false;

        }

        Console.ForegroundColor = ConsoleColor.Blue;

        Console.WriteLine("{0,-10} | {1,-40} | {2,-30}", "ID", "Nome", "Fabricante");

        Console.WriteLine("------------------------------------------------------------------------------");

        for (int i = 0; i < listaIdsEquipamentos.Count; i++)
        {
            Console.WriteLine("{0,-10} | {1,-40} | {2,-30}", listaIdsEquipamentos[i], listaNomeEquipamentos[i], listaFabricantesEquipamentos[i]);
        }
        Console.ResetColor();

        return true;
    }

    static void inserirNovoEquipamento()
    {
        MostrarCabecalho("Cadastro de Equipamentos", "Visualizando Equipamentos");

        GravarEquipamento(ContadorDeEquipamento, "INSERIR");

        IncrementarIdEquipamento();

        ApresentarMensagem("Equipamento inserido com sucesso!", ConsoleColor.Green);


    }

    static void IncrementarIdEquipamento()
    {
        ContadorDeEquipamento++;
    }

    static string ApresentarMenuCadastroEquipamento()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Cadastro de equipamentos");
        Console.WriteLine();
        Console.WriteLine("Digite 1 para inserir um novo equipamento");
        Console.WriteLine("Digite 2 para visualizar equipamentos cadastrados ");
        Console.WriteLine("Digite 3 para editar equipamentos");
        Console.WriteLine("Digite 4 para excluir equipamento");
        Console.WriteLine();
        Console.WriteLine("Digite s para voltar para o menu principal");

        string opcao = Console.ReadLine();

        return opcao;

    }

    static string ApresentarMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("Gestão de Equipamentos 1.0 ");
        Console.WriteLine();
        Console.WriteLine("Digite 1 para Cadastrar Equipamentos");
        Console.WriteLine();
        Console.WriteLine("Digite 2 para Controlar Chamadas");
        Console.WriteLine();
        Console.WriteLine("Digite S para sair");

        string opcao = Console.ReadLine();

        return opcao;
    }

    static void CadastrarAlgunEquipamentosAutomaticamente()
    {
        listaIdsEquipamentos.Add(ContadorDeEquipamento);
        listaNomeEquipamentos.Add("Impressora");
        listaPrecoEquipamentos.Add(1500);
        listaNumeroSerieEquipamentos.Add("123-abc");
        listaDatasFabricacaoEquipamentos.Add("12/12/2022");
        listaFabricantesEquipamentos.Add("LexMark");
        ContadorDeEquipamento++;
    }

    static void CadastrarAlgunsChamadosAutomaticamente()
    {
        listaIdsChamado.Add(ContadorDeChamados);
        listaIdsEquipamentoChamado.Add(listaIdsEquipamentos[0]);
        listaTitulosChamado.Add("Impressão Fraca");
        listaDescricoesChamado.Add("Mesmo Trocando o tonner, impressao continua fraca");
        listaDatasAberturaChamado.Add("04/04/2023");

        ContadorDeChamados++;
    }
}
