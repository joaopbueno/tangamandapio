using System;
using System.Collections.Generic;
using System.Linq;

    class Morador
    {
        public double cpfMorador { get; set; }
        public string nomeCompleto { get; set; }
        public byte dependentes { get; set; }
        public double rendaFamiliar { get; set; }
        public double telefone { get; set; }
        public string endereco { get; set; }
    }
    class Program
    {
        static Dictionary<double, Morador> _listaMoradoresFaixa1 = new Dictionary<double, Morador>();
        static Dictionary<double, Morador> _listaMoradoresFaixa2 = new Dictionary<double, Morador>();
        static Queue<Morador> _filaDeEsperaFaixa1 = new Queue<Morador>();
        static Queue<Morador> _filaDeEsperaFaixa2 = new Queue<Morador>();
        static double salario;
        static byte m1;
        static byte m2;
        static byte N;
        static void Main(string[] args)
        {
            configuracoesDoPrograma();

            Console.Clear();
            try
            {
                double cond;
                do
                {
                    Console.Clear();
                    Console.WriteLine("0 – Configurações do programa");
                    Console.WriteLine("1 – Cadastrar morador");
                    Console.WriteLine("2 – Imprimir lista de moradores cadastrados");
                    Console.WriteLine("\t2.1 – Listagem simples (apenas CPF e nome do morador)");
                    Console.WriteLine("\t2.2 – Listagem completa (todos os dados)");
                    Console.WriteLine("3 – Imprimir fila de espera");
                    Console.WriteLine("4 – Pesquisar morador");
                    Console.WriteLine("5 – Desistência/Exclusão ");
                    Console.WriteLine("6 – Sorteio ");
                    Console.WriteLine("7 - Quantidade de cadastros");
                    Console.Write("8 – Sair\nOpção: ");
                    cond = double.Parse(Console.ReadLine());

                    Console.Clear();
                    switch (cond)
                    {
                        case 0:
                            configuracoesDoPrograma();
                            break;
                        case 1:
                            cadastroMorador();
                            break;
                        case 2.1:
                            listagemSimples();
                            break;
                        case 2.2:
                            listagemCompleta();
                            break;
                        case 3:
                            filaDeEspera();
                            break;
                        case 4:
                            pesquisaDeMorador();
                            break;
                        case 5:
                            desistenciaExclusao();
                            break;
                        case 6:
                            Sorteio();
                            break;
                        case 7:
                            retornaQntRegistro();
                            break;
                        default:
                            if (cond != 8)
                            {
                                Console.WriteLine("Opção inválida!");
                                Console.ReadKey();
                            }
                            break;
                    }
                } while (cond != 8);
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.ReadKey();
            }
            catch (NullReferenceException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.ReadKey();
            }

            Console.WriteLine("FIM DO PROGRAMA");

            Console.ReadKey();
        }
        /*
         * CONFIGURAÇÕES DO PROGRAMA
         * 
         */
        public static void configuracoesDoPrograma()
        {
            Console.Clear();
            Console.WriteLine("\tConfigurações");
            try
            {
                Console.Write("Salario minimo: ");
                salario = double.Parse(Console.ReadLine());
                Console.Write("Max moradores FAIXA1: ");
                m1 = byte.Parse(Console.ReadLine());
                Console.Write("Max moradores FAIXA2: ");
                m2 = byte.Parse(Console.ReadLine());
                Console.Write("Max lista de espera: ");
                N = byte.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tente configurar novamente!");
                Console.ResetColor();

                Console.ReadKey();
            }
            catch (NullReferenceException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tente configurar novamente!");
                Console.ResetColor();

                Console.ReadKey();
            }
        }
        /*
         *  CADASTRO DE MORADORES
         * 
         */
        public static void cadastroMorador()
        {
            try
            {
                Console.Clear();
                Morador M = new Morador();
                Console.Write("Nome completo: ");
                M.nomeCompleto = Console.ReadLine();
                Console.Write("CPF (apenas numeros): ");
                M.cpfMorador = double.Parse(Console.ReadLine());
                Console.Write("Endereço: ");
                M.endereco = Console.ReadLine();
                Console.Write("Telefone (Apenas numeros):");
                M.telefone = double.Parse(Console.ReadLine());
                Console.Write("Renda familiar: ");
                M.rendaFamiliar = double.Parse(Console.ReadLine());
                Console.Write("Numero de dependentes: ");
                M.dependentes = byte.Parse(Console.ReadLine());
                Console.Clear();
                // ADICIONANDO NA LISTA DE MORADORES
                if (!_listaMoradoresFaixa1.Keys.Contains(M.cpfMorador) && !_listaMoradoresFaixa2.Keys.Contains(M.cpfMorador))
                {
                    if (M.rendaFamiliar <= salario)
                    {
                        if (_listaMoradoresFaixa1.Count < m1)
                        {
                            _listaMoradoresFaixa1.Add(M.cpfMorador, M);
                        }
                        else if (_filaDeEsperaFaixa1.Count < N)
                        {
                            _filaDeEsperaFaixa1.Enqueue(M);
                            Console.WriteLine("Morador cadastrado na lista de espera da FAIXA1");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("VAGAS ESGOTADAS");
                            Console.ReadKey();
                        }
                    }
                    else if (M.rendaFamiliar > salario && M.rendaFamiliar <= (salario * 3))
                    {
                        if (_listaMoradoresFaixa2.Count < m2)
                        {
                            _listaMoradoresFaixa2.Add(M.cpfMorador, M);
                        }
                        else if (_filaDeEsperaFaixa2.Count < N)
                        {
                            _filaDeEsperaFaixa2.Enqueue(M);
                            Console.WriteLine("Morador cadastrado na lista de espera da FAIXA2");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("VAGAS ESGOTADAS");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Renda Familiar superior a 3 salários mínimos, este morador não pode parcipar do sorteio!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("CPF já cadastrado!");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tente cadastrar outro CPF");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tente cadastrar novamente!");
                Console.ResetColor();
                Console.ReadKey();
            }
            catch (NullReferenceException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tente cadastrar novamente!");
                Console.ResetColor();

                Console.ReadKey();
            }
        }
        /*
         * IMPRIMIR A LISTA SIMPLES
         * 
         */
        public static void listagemSimples()
        {
            Console.Clear();
            if (_listaMoradoresFaixa1.Count > 0 || _listaMoradoresFaixa2.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LISTAGEM SIMPLES DE MORADORES (FAIXA1)");
                Console.ResetColor();
                Console.WriteLine("=================================================================================");
                foreach (Morador mor in _listaMoradoresFaixa1.Values)
                {
                    Console.WriteLine("CPF: {0}\tNome: {1}\tRenda Familiar: {2}", mor.cpfMorador, mor.nomeCompleto, mor.rendaFamiliar);
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LISTAGEM SIMPLES DE MORADORES (FAIXA2)");
                Console.ResetColor();
                Console.WriteLine("=================================================================================");
                foreach (Morador mor in _listaMoradoresFaixa2.Values)
                {
                    Console.WriteLine("CPF: {0}\tNome: {1}\tRenda Familiar: {2}", mor.cpfMorador, mor.nomeCompleto, mor.rendaFamiliar);
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                }
            }
            else
            {
                Console.WriteLine("Lista de moradores vazia!");
            }
            Console.ReadKey();
        }
        /*
         * IMPRIMIR A LISTA COMPLETA
         *
         */
        public static void listagemCompleta()
        {
            Console.Clear();
            if (_listaMoradoresFaixa1.Count > 0 || _listaMoradoresFaixa2.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LISTAGEM COMPLETA DE MORADORES (FAIXA1)");
                Console.ResetColor();
                Console.WriteLine("=================================================================================");
                foreach (Morador mor in _listaMoradoresFaixa1.Values)
                {
                    Console.WriteLine("CPF: {0}\tNome: {1}\nQtde. Dependentes: {2}\tRenda Familiar: {3}\nTelefone: {4}\nEndereço: {5}"
                        , mor.cpfMorador, mor.nomeCompleto, mor.dependentes, mor.rendaFamiliar, mor.telefone, mor.endereco);
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LISTAGEM COMPLETA DE MORADORES (FAIXA2)");
                Console.ResetColor();
                Console.WriteLine("=================================================================================");
                foreach (Morador mor in _listaMoradoresFaixa2.Values)
                {
                    Console.WriteLine("CPF: {0}\tNome: {1}\nQtde. Dependentes: {2}\tRenda Familiar: {3}\nTelefone: {4}\nEndereço: {5}"
                        , mor.cpfMorador, mor.nomeCompleto, mor.dependentes, mor.rendaFamiliar, mor.telefone, mor.endereco);
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                }
            }
            else
            {
                Console.WriteLine("Lista de moradores vazia!");
            }
            Console.ReadKey();
        }
        /*
         * IMPRIMINDO FILA DE ESPERA
         * 
         */
        public static void filaDeEspera()
        {
            Console.Clear();
            if (_filaDeEsperaFaixa1.Count > 0 || _filaDeEsperaFaixa2.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("LISTA DE ESPERA (FAIXA1)");
                Console.ResetColor();
                Console.WriteLine("=================================================================================");
                foreach (Morador mor in _filaDeEsperaFaixa1)
                {
                    Console.WriteLine("CPF: {0}\tNome: {1}\tRenda Familiar: {2}", mor.cpfMorador, mor.nomeCompleto, mor.rendaFamiliar);
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                }
                Console.WriteLine("LISTA DE ESPERA (FAIXA2)");
                Console.WriteLine("=================================================================================");
                foreach (Morador mor in _filaDeEsperaFaixa2)
                {
                    Console.WriteLine("CPF: {0}\tNome: {1}\tRenda Familiar: {2}", mor.cpfMorador, mor.nomeCompleto, mor.rendaFamiliar);
                    Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                }
            }
            else
            {
                Console.WriteLine("Fila de Espera vazia!");
            }
            Console.ReadKey();
        }
        /*
         * PESQUISA DE MORADOR POR CPF
         * OU IMPRIMIR DADOS DO MORADOR VENCEDOR DO SROTEIO
         * 
         */
        public static void pesquisaDeMorador(double search = 0)
        {            
            if (_listaMoradoresFaixa1.Count > 0 || _listaMoradoresFaixa2.Count > 0)
            {
                bool achou = false;
                if (search == 0)
                {
                    Console.Clear();
                    try
                    {
                        Console.Clear();
                        Console.Write("Digite o CPF do morador (Apenas numeros): ");
                        search = double.Parse(Console.ReadLine());
                    }
                    catch (FormatException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tente pesquisar novamente!");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tente pesquisar novamente!");
                        Console.ResetColor();

                        Console.ReadKey();
                    }
                }
                foreach (var aux in _listaMoradoresFaixa1)
                {
                    if (search == aux.Key)
                    {
                        Morador mor = aux.Value;
                        Console.WriteLine("CPF: {0}\tNome: {1}\nQtde. Dependentes: {2}\tRenda Familiar: {3}\nTelefone: {4}\nEndereço: {5}"
                                , mor.cpfMorador, mor.nomeCompleto, mor.dependentes, mor.rendaFamiliar, mor.telefone, mor.endereco);
                        Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                        achou = true;
                    }
                }
                foreach (var aux2 in _listaMoradoresFaixa2)
                {
                    if (search == aux2.Key)
                    {
                        Morador mor = aux2.Value;
                        Console.WriteLine("CPF: {0}\tNome: {1}\nQtde. Dependentes: {2}\tRenda Familiar: {3}\nTelefone: {4}\nEndereço: {5}"
                                , mor.cpfMorador, mor.nomeCompleto, mor.dependentes, mor.rendaFamiliar, mor.telefone, mor.endereco);
                        Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                        achou = true;
                    }
                }
                if (!achou)
                {
                    Console.WriteLine("Morador não encontrado!");
                }
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Lista de moradores vazia!");
                Console.ReadKey();
            }
        }
        /*
         * EXCLUINDO MORADOR
         * 
         */
        public static void desistenciaExclusao()
        {
            Console.Clear();
            if (_listaMoradoresFaixa1.Count > 0 || _listaMoradoresFaixa2.Count > 0)
            {
                bool achou = false;
                try
                {
                    Console.Write("CPF do morador (Apenas numros): ");
                    double search = double.Parse(Console.ReadLine());
                    if(_listaMoradoresFaixa1.Count > 0)
                    {
                        foreach (var aux in _listaMoradoresFaixa1)
                        {
                            if (search == aux.Key)
                            {
                                Morador mor = aux.Value;
                                Console.WriteLine("CPF: {0}\tNome: {1}\nQtde. Dependentes: {2}\tRenda Familiar: {3}\nTelefone: {4}\nEndereço: {5}"
                                        , mor.cpfMorador, mor.nomeCompleto, mor.dependentes, mor.rendaFamiliar, mor.telefone, mor.endereco);
                                Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                                achou = true;
                                
                            }
                        }
                        if (achou)
                        {
                            Console.WriteLine("Tem certeza que deseja excluir o morador? [S/N]");
                            string res = Console.ReadLine().ToUpper();

                            if (res == "S")
                            {
                                _listaMoradoresFaixa1.Remove(search);
                                if (_filaDeEsperaFaixa1.Count > 0)
                                {
                                    Morador morador = _filaDeEsperaFaixa1.Dequeue();
                                    _listaMoradoresFaixa1.Add(morador.cpfMorador, morador);
                                }
                                Console.WriteLine("Morador removido!");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Nenhum morador foi removido!");
                            }
                        }
                    }
                    if(_listaMoradoresFaixa2.Count > 0 && !achou)
                    {
                        foreach (var aux in _listaMoradoresFaixa2)
                        {
                            if (search == aux.Key)
                            {
                                Morador mor = aux.Value;
                                Console.WriteLine("CPF: {0}\tNome: {1}\nQtde. Dependentes: {2}\tRenda Familiar: {3}\nTelefone: {4}\nEndereço: {5}"
                                        , mor.cpfMorador, mor.nomeCompleto, mor.dependentes, mor.rendaFamiliar, mor.telefone, mor.endereco);
                                Console.WriteLine("-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.");
                                achou = true;            
                            }
                        }
                        if (achou)
                        {
                            Console.WriteLine("Tem certeza que deseja excluir o morador? [S/N]");
                            string res = Console.ReadLine().ToUpper();

                            if (res == "S")
                            {
                                _listaMoradoresFaixa2.Remove(search);
                                if (_filaDeEsperaFaixa2.Count > 0)
                                {
                                    Morador morador = _filaDeEsperaFaixa2.Dequeue();
                                    _listaMoradoresFaixa2.Add(morador.cpfMorador, morador);
                                }

                                Console.WriteLine("Morador removido!");
                            }
                            else
                            {
                                Console.WriteLine("Nenhum morador foi removido!");
                            }
                        }
                    }                    
                    if (!achou)
                    {
                        Console.WriteLine("Morador não encontrado!");
                    }
                }
                catch (FormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tente excluir novamente!");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                catch (NullReferenceException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tente excluir novamente!");
                    Console.ResetColor();

                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Nenhum morador cadastrado!");
            }
            Console.ReadKey();
        }
        public static void Sorteio()
        {
            Console.Clear();
            List<double> _sortFaixa1 = new List<double>();
            List<double> _sortFaixa2 = new List<double>();
            Random numaAleatiorio = new Random();
            if (_listaMoradoresFaixa1.Count == 0 && _listaMoradoresFaixa2.Count == 0)
            {
                Console.WriteLine("Sem moradores inscritos!");
            }
            else
            {
                /*
                 * SORTEIO DO CAMPEÃO
                 * 
                 */
                if (_listaMoradoresFaixa1.Count > 0)
                {
                    foreach (var CPF in _listaMoradoresFaixa1.Keys)
                    {
                        _sortFaixa1.Add(CPF);
                    }
                    int alet = numaAleatiorio.Next(0, _sortFaixa1.Count);
                    Console.WriteLine("O GANHADOR DA FAIXA 1 : ");
                    pesquisaDeMorador(_sortFaixa1[alet]);
                }
                else
                {
                    Console.WriteLine("Sem inscritos para a FAIXA 1!");
                }
                if (_listaMoradoresFaixa2.Count > 0)
                {
                    foreach (var CPF in _listaMoradoresFaixa2.Keys)
                    {
                        _sortFaixa2.Add(CPF);
                    }
                    int alet = numaAleatiorio.Next(0, _sortFaixa2.Count);
                    Console.WriteLine("O GANHADOR DA FAIXA 2 : ");
                    pesquisaDeMorador(_sortFaixa2[alet]);
                }
                else
                {
                    Console.WriteLine("Sem inscristos para a FAIXA 2!");
                }
            }
            Console.ReadKey();
        }
        /*
         * RETORNA QUANTIDADE DE MORADORES CADASTRADOS
         * Funcionalidade que o professor pediu!
         * 
         */
        public static void retornaQntRegistro()
        {
            Console.Clear();
            Console.WriteLine("Inscritos na Faixa 1:\n {0} e restam {1} vagas.", _listaMoradoresFaixa1.Count, m1 -_listaMoradoresFaixa1.Count);
            Console.WriteLine("Fila de espera da Faixa 1:\n {0} e restam {1} vagas", _filaDeEsperaFaixa1.Count, N - _filaDeEsperaFaixa1.Count);
            Console.WriteLine("Inscritos na Faixa 2:\n {0} e restam {1} vagas", _listaMoradoresFaixa2.Count, m2 - _listaMoradoresFaixa2.Count);
            Console.WriteLine("Fila de espera da Faixa 2:\n {0} e restam {1} vagas", _filaDeEsperaFaixa2.Count, N - _filaDeEsperaFaixa2.Count);
            Console.ReadKey();
        }
    }
