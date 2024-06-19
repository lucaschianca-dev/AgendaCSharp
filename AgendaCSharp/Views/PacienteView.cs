using AgendaCSharp.DTOs;
using AgendaCSharp.Services;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace AgendaCSharp.Views
{
    public class PacienteView
    {
        private PacienteService _pacienteService;

        private const string Remover = "\nPaciente removido com sucesso!\n";
        private const string Sucesso = "\nPaciente cadastrado com sucesso!\n";
        private const string InformeDadosPaciente = "\n ► Informe os dados do paciente:\n";
        private const string CpfPrompt = "\nCPF: ";
        private const string NomePrompt = "Nome: ";
        private const string DataNascimentoPrompt = "Data de Nascimento (DDMMAAAA): ";
        private const string DataInvalidaMensagem = "Data inválida! Tente novamente.\n";
        private const string LinhaSeparadora = "\n---------------------------------------------------------------------\n";
        private const string LogoListaDeConsultas = @"
  _     _     _              _        ____            _            _            
 | |   (_)___| |_ __ _    __| | ___  |  _ \ __ _  ___(_) ___ _ __ | |_ ___  ___ 
 | |   | / __| __/ _` |  / _` |/ _ \ | |_) / _` |/ __| |/ _ \ '_ \| __/ _ \/ __|
 | |___| \__ \ || (_| | | (_| |  __/ |  __/ (_| | (__| |  __/ | | | ||  __/\__ \
 |_____|_|___/\__\__,_|  \__,_|\___| |_|   \__,_|\___|_|\___|_| |_|\__\___||___/
                                                                                
";

        public PacienteView()
        {
        }

        public void SetPacienteService(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        public PacienteDTO CapturarDados()
        {
            ExibirMensagemAqua(InformeDadosPaciente);

            string cpf = CapturarEntrada(CpfPrompt);
            string nome = CapturarEntrada(NomePrompt);
            DateTime dataDeNascimento = CapturarDataDeNascimento();

            return new PacienteDTO { Cpf = cpf, Nome = nome, DataDeNascimento = dataDeNascimento, Consultas = new List<ConsultaDTO>() };
        }

        private string CapturarEntrada(string mensagem)
        {
            ExibirMensagem(mensagem);
            return Console.ReadLine();
        }

        private DateTime CapturarDataDeNascimento()
        {
            while (true)
            {
                ExibirMensagem(DataNascimentoPrompt);
                var dataInput = Console.ReadLine();

                if (DateTime.TryParseExact(dataInput, "ddMMyyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dataDeNascimento))
                {
                    return dataDeNascimento;
                }
                else
                {
                    ExibirMensagemErro("ERRO", DataInvalidaMensagem);
                }
            }
        }

        public void ExibirPacientes(List<PacienteDTO> pacientes)
        {
            ExibeLogoListaDeConsultas();
            ExibirMensagemAqua(LinhaSeparadora, Color.AntiqueWhite);
            ConstruirPlanilha("CPF", "Nome", "Dt. Nasc.", "Idade");
            ExibirMensagemAqua(LinhaSeparadora, Color.AntiqueWhite);

            foreach (var paciente in pacientes)
            {
                string dataNascimentoFormatada = paciente.DataDeNascimento.ToString("dd/MM/yyyy").PadRight(18);
                ExibirMensagemAqua($"{paciente.Cpf.PadRight(11)} {paciente.Nome.PadRight(35)} {dataNascimentoFormatada} {paciente.Idade}\n", Color.AntiqueWhite);

                foreach (var consulta in paciente.Consultas.Where(c => c.Data > DateTime.Now || (c.Data == DateTime.Now.Date && c.HoraInicial > DateTime.Now.TimeOfDay)))
                {
                    ExibirMensagemAqua($"            Agendado para: {consulta.Data:dd/MM/yyyy}\n            {consulta.HoraInicial:hh\\:mm} às {consulta.HoraFinal:hh\\:mm}\n", Color.Cyan);
                }
            }
        }

        public string CapturarCpfParaRemocao()
        {
            ExibirMensagem("\n" + CpfPrompt);
            return Console.ReadLine();
        }

        public void ExibirErro(string erro, string mensagem)
        {
            ExibirMensagemAqua($"\n[{erro}] {mensagem}", Color.Crimson);
        }

        public void ConstruirPlanilha(string cpf, string nome, string dataNascimento, string idade)
        {
            ExibirMensagemAqua($"{cpf,-11} {nome,-32} {dataNascimento,12} {idade,11}", Color.Aqua);
        }

        public void ExibirMensagem(string mensagem)
        {
            Console.Write(mensagem);
        }

        public void ExibirMensagemAqua(string mensagem, Color cor)
        {
            Console.Write(mensagem, cor);
        }

        public void ExibirMensagemVerde(string mensagem)
        {
            ExibirMensagemAqua(mensagem, Color.LimeGreen);
        }

        public void ExibirMensagemVermelho(string mensagem)
        {
            ExibirMensagemAqua(mensagem, Color.Crimson);
        }

        public void ExibirMensagemAqua(string mensagem)
        {
            ExibirMensagemAqua(mensagem, Color.Aqua);
        }

        public void ExibirMensagemErro(string erro, string mensagem)
        {
            ExibirMensagemAqua($"\n|{erro}| {mensagem}\n", Color.Crimson);
        }

        public void ExibirMensagemSimboloAqua(string simbolo, string mensagem)
        {
            ExibirMensagemAqua(simbolo, Color.Aqua);
            ExibirMensagem(mensagem);
        }

        public void ExibeLogoListaDeConsultas()
        {
            ExibirMensagemAqua(LogoListaDeConsultas, Color.Aqua);
        }

        public void MensagemCadastrarOutroPaciente()
        {
            ExibirMensagemAqua("\n► ");
            ExibirMensagem("Deseja cadastrar outro paciente? (");
            ExibirMensagemAqua("y");
            ExibirMensagem("/");
            ExibirMensagemAqua("n");
            ExibirMensagem(")\n");
        }

        public void MensagemOpcaoInvalida()
        {
            ExibirMensagemVermelho(" ▲ Opção inválida. ");
            ExibirMensagem("Por favor, digite '");
            ExibirMensagemAqua("y");
            ExibirMensagem("' para sim ou '");
            ExibirMensagemAqua("n");
            ExibirMensagem("' para não.\n");
        }

        public void MensagemSucessoCadastro()
        {
            ExibirMensagemVerde(Sucesso);
        }

        public void MensagemRemoverPaciente()
        {
            ExibirMensagemVerde(Remover);
        }

        public void MensagemTentarNovamente()
        {
            ExibirMensagem("Deseja tentar novamente? (");
            ExibirMensagemAqua("y");
            ExibirMensagem("/");
            ExibirMensagemAqua("n");
            ExibirMensagem(")\n");
        }
    }
}
