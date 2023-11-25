using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueObj;
    [SerializeField] private GameObject dialogueQuestionsObj;
    [SerializeField] private Text messageText;
    [SerializeField] private Text actorNameText;

    [SerializeField] private Text firstQuestion;
    [SerializeField] private Text secondQuestion;
    [SerializeField] private Text thirdQuestion;

    private PlayerMovement _player;

    private GameManager _gm;

    private string[] sentences;
    private int index;

    private int baseTextIndex;
    public float typingSpeed;

    private bool _canAsk;

    public enum ProblemType { DEPRESSION, BURNOUT, ANXIETY }
    private readonly string[] baseTexts = new string[10]
    {
        "Percorro os caminhos enevoados da minha própria escuridão, onde os murmúrios silenciosos se misturam ao peso das sombras que carrego...",
        "Minha alma é um jardim de emoções emaranhadas, onde os ventos inconstantes sussurram segredos de uma tormenta interior...",
        "Cada sorriso é um arco-íris desbotado, pintando o céu da minha existência com matizes de uma paleta emocional desafiadora...",
        "Caminho por corredores de pensamentos inquietos, onde as paredes ecoam as pegadas pesadas de uma jornada além do que os olhos podem ver...",
        "Sou um viajante em uma dança eterna, entre a luz efêmera da alegria e as sombras que se estendem como uma tapeçaria de incertezas...",
        "As páginas do meu ser são um manuscrito borrado, onde as palavras da tristeza se misturam com as tintas da incerteza...",
        "Meu coração é um farol oscilante, lançando luz sobre mares tumultuados de pensamentos que dançam ao som de uma melodia desconhecida...",
        "Navego pelas águas inexploradas da minha própria alma, onde as correntes da preocupação e os redemoinhos da exaustão se entrelaçam...",
        "Cada suspiro é uma nota solitária em uma sinfonia interna, onde as escalas de emoções se desdobram em melodias entrelaçadas...",
        "Sou um artista de expressões veladas, pintando telas com pincéis feitos de anseios e sombras, onde as cores se confundem em um quadro de emoções indefiníveis..."
    };

    void Start()
    {
        baseTextIndex = Random.Range(0, baseTexts.Length - 1);
        _gm = FindObjectOfType<GameManager>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    public void Speech(string actorName, bool canAsk)
    {
        dialogueObj.SetActive(true);
        actorNameText.text = actorName;
        _canAsk = canAsk;
        _player.canMove = false;

        sentences = new string[1] { baseTexts.ElementAt(baseTextIndex) };
        StartCoroutine(TypingSentence());
    }
    public void ReturnQuestions()
    {
        dialogueQuestionsObj.SetActive(true);

        switch (baseTextIndex)
        {
            case 0:
                firstQuestion.text = "Você descreve caminhos enevoados na sua própria escuridão. Essa neblina é algo que parece persistir, ou há momentos de clareza entre as sombras?";
                secondQuestion.text = "Os murmúrios silenciosos que você menciona são algo que você tenta compreender, ou é mais uma presença constante e incompreensível?";
                thirdQuestion.text = "O peso das sombras que você carrega é algo que sente que pode aliviar, ou é mais uma carga que persiste independente dos esforços?";
                break;
            case 1:
                firstQuestion.text = "Você descreve sua alma como um jardim de emoções emaranhadas. Essas emoções são algo que você tenta desembaraçar, ou é mais uma convivência com a complexidade emocional?";
                secondQuestion.text = "Os ventos inconstantes que sussurram segredos de uma tormenta interior são algo que você tenta compreender, ou é mais uma experiência misteriosa e imprevisível?";
                thirdQuestion.text = "A tormenta interior que você menciona é algo que sente que pode acalmar, ou é mais uma força que se manifesta independentemente dos esforços?";
                break;
            case 2:
                firstQuestion.text = "Você descreve cada sorriso como um arco-íris desbotado. Esse desvanecimento é algo que você sente que pode revitalizar, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Pintar o céu da sua existência com matizes de uma paleta emocional desafiadora sugere uma variedade de emoções intensas. Como você lida com essa diversidade emocional?";
                thirdQuestion.text = "Essa paleta emocional desafiadora é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 3:
                firstQuestion.text = "Você descreve caminhar por corredores de pensamentos inquietos. Esses pensamentos são algo que você sente que pode acalmar, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar que as paredes ecoam as pegadas pesadas de uma jornada além do que os olhos podem ver, isso sugere uma jornada emocional intensa. Como você lida com essa carga emocional?";
                thirdQuestion.text = "Esses corredores de pensamentos inquietos são algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 4:
                firstQuestion.text = "Você descreve sua jornada como uma dança entre a luz efêmera da alegria e as sombras de incertezas. Essa dança é algo que você sente que pode controlar, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar as sombras que se estendem como uma tapeçaria de incertezas, isso sugere uma presença constante de dúvidas. Como você lida com essa tapeçaria de incertezas?";
                thirdQuestion.text = "Essa dança entre luz e sombras é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 5:
                firstQuestion.text = "Você descreve as páginas do seu ser como um manuscrito borrado, onde as palavras da tristeza se misturam com as tintas da incerteza. Esse manuscrito é algo que você sente que pode reescrever, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar que as palavras da tristeza se misturam com as tintas da incerteza, isso sugere uma combinação de emoções intensas. Como você lida com essa mistura emocional?";
                thirdQuestion.text = "Esse manuscrito borrado é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 6:
                firstQuestion.text = "Você descreve seu coração como um farol oscilante, lançando luz sobre mares tumultuados de pensamentos. Esse farol é algo que você sente que pode controlar, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar mares tumultuados de pensamentos que dançam ao som de uma melodia desconhecida, isso sugere uma experiência emocional intensa. Como você lida com essa dança emocional desconhecida?";
                thirdQuestion.text = "Esse farol oscilante é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 7:
                firstQuestion.text = "Você descreve navegar pelas águas inexploradas da sua própria alma, onde as correntes da preocupação e os redemoinhos da exaustão se entrelaçam. Essa navegação é algo que você sente que pode direcionar, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar as correntes da preocupação e os redemoinhos da exaustão, isso sugere uma experiência emocional intensa. Como você lida com essa interação entre preocupação e exaustão?";
                thirdQuestion.text = "Essa navegação por águas inexploradas é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 8:
                firstQuestion.text = "Você descreve cada suspiro como uma nota solitária em uma sinfonia interna, onde as escalas de emoções se desdobram em melodias entrelaçadas. Essas melodias são algo que você sente que pode modular, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar que cada suspiro é uma nota solitária em uma sinfonia interna, isso sugere uma experiência emocional intensa. Como você lida com a solidão dessas notas solitárias e a complexidade das melodias entrelaçadas?";
                thirdQuestion.text = "Essa sinfonia interna é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
            case 9:
                firstQuestion.text = "Você se descreve como um artista de expressões veladas, pintando telas com pincéis feitos de anseios e sombras, onde as cores se confundem em um quadro de emoções indefiníveis. Essa expressão artística é algo que você sente que pode moldar, ou é mais uma experiência persistente em sua jornada emocional?";
                secondQuestion.text = "Ao mencionar pincéis feitos de anseios e sombras, onde as cores se confundem em um quadro de emoções indefiníveis, isso sugere uma experiência emocional intensa. Como você lida com essa confusão e a indefinição das emoções em seu quadro interno?";
                thirdQuestion.text = "Essa expressão artística é algo que você compartilha com outras pessoas, ou é mais uma experiência íntima e isolada?";
                break;
        };
    }

    public void ReturnResponse(int buttonId)
    {
        dialogueQuestionsObj.SetActive(false);
        _canAsk = false;

        var corruption = _gm.corruption;

        switch (baseTextIndex)
        {
            case 0:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "A neblina parece persistir, como se a escuridão fosse constante e envolvente, dificultando encontrar clareza...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Há momentos de clareza, mas a neblina muitas vezes retorna, especialmente sob o peso das sombras que carrego...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A neblina é como uma antecipação constante do desconhecido, criando um peso adicional às sombras que carrego...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "São constantes e incompreensíveis, como se representassem uma melancolia persistente na minha mente...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreendê-los, mas sob o peso das sombras, esses murmúrios muitas vezes se tornam sobrecarregados e difíceis de decifrar...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "São uma presença constante, criando uma atmosfera de inquietação, como se os murmúrios estivessem sempre à espreita...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma carga persistente, como se as sombras se entrelaçassem comigo, tornando difícil encontrar alívio...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Sinto que posso aliviar o peso em alguns momentos, mas sob certas circunstâncias, as sombras se intensificam, tornando a carga mais difícil de suportar...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "É uma carga que parece aumentar em situações específicas, como se as sombras ganhassem mais peso, aumentando a ansiedade...";
                                break;
                        }
                        break;
                }
                break;
            case 1:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Tento desembaraçar, mas muitas vezes as emoções formam um emaranhado difícil de compreender, contribuindo para uma sensação de desânimo...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Sinto que há momentos em que consigo desembaraçar as emoções, mas sob os ventos inconstantes, o emaranhado retorna, contribuindo para o desgaste emocional...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "É uma convivência constante com a complexidade emocional, como se os ventos inconstantes aumentassem a intensidade do emaranhado, gerando ansiedade...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Sinto que são misteriosos e imprevisíveis, como se os segredos da tormenta interior fossem difíceis de decifrar, contribuindo para a sensação de melancolia...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender, mas sob a pressão dos ventos inconstantes, os segredos da tormenta muitas vezes permanecem obscuros, aumentando o desgaste emocional...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "É uma experiência imprevisível, onde os segredos da tormenta interior são sussurrados pelos ventos inconstantes, contribuindo para a confusão...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma força que se manifesta independentemente dos esforços, como se a tormenta interior fosse difícil de acalmar, contribuindo para a sensação de desesperança...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Sinto que posso acalmar a tormenta em alguns momentos, mas sob certas circunstâncias, ela se intensifica, contribuindo para o desgaste emocional...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "É uma força imprevisível, onde a tormenta interior aumenta em situações específicas, contribuindo para a intensificação das confusões...";
                                break;
                        }
                        break;
                }
                break;
            case 2:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento trazer mais vivacidade aos sorrisos, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Revitalizar os sorrisos é algo que tento, mas a paleta emocional desafiadora pode obscurecer a vivacidade, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com a vivacidade dos sorrisos pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa diversidade é um desafio constante, como se as nuances emocionais contribuíssem para a complexidade interna, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com a diversidade emocional, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A diversidade emocional é uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar essa vivacidade interna...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 3:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento acalmar esses pensamentos inquietos, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Acalmar esses pensamentos é algo que tento, mas a carga emocional pesada pode obscurecer a tranquilidade, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com pensamentos inquietos pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa carga emocional é um desafio constante, como se as pegadas pesadas ecoassem a complexidade interna, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com a carga emocional, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A carga emocional é uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar essa jornada inquieta...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 4:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento controlar essa dança entre a luz e as sombras, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Controlar essa dança é algo que tento, mas as sombras de incertezas podem obscurecer a alegria, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com essa dança pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa tapeçaria de incertezas é um desafio constante, como se as dúvidas estendessem a complexidade interna, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com as incertezas, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "As dúvidas são uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar essa dança entre luz e sombras...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 5:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento reescrever esse manuscrito, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Reescrever esse manuscrito é algo que tento, mas as tintas da incerteza podem obscurecer a clareza emocional, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com esse manuscrito pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa mistura emocional é um desafio constante, como se as palavras da tristeza se entrelaçassem com as tintas da incerteza, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "TTento compreender e lidar com a mistura emocional, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A mistura emocional é uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar esse manuscrito borrado...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 6:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento controlar esse farol oscilante, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Controlar esse farol é algo que tento, mas os mares tumultuados de pensamentos podem obscurecer a luz, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com esse farol oscilante pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa dança emocional desconhecida é um desafio constante, como se os pensamentos tumultuados dançassem ao ritmo da complexidade interna, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com essa dança emocional, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A dança emocional é uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar esse farol oscilante...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 7:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento direcionar essa navegação, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Direcionar essa navegação é algo que tento, mas os redemoinhos da exaustão podem obscurecer o caminho, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com essa navegação pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa interação é um desafio constante, como se as correntes da preocupação se entrelaçassem com os redemoinhos da exaustão, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com essa interação, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A interação entre preocupação e exaustão é uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar essa navegação por águas inexploradas...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 8:
                switch (buttonId)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento modular essas melodias, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Modular essas melodias é algo que tento, mas a solidão das notas solitárias pode obscurecer a harmonia, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com essas melodias pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com a solidão dessas notas solitárias é um desafio constante, como se as escalas de emoções se desdobrassem em uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com a solidão das notas solitárias, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A solidão das notas solitárias e a complexidade das melodias são uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar essa sinfonia interna...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
            case 9:
                switch (corruption)
                {
                    case 0:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Às vezes, tento moldar essa expressão artística, mas há momentos em que a complexidade emocional persiste, tornando a jornada mais desafiadora...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Moldar essa expressão artística é algo que tento, mas a confusão das cores pode obscurecer a clareza, tornando a navegação emocional mais complexa...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "Lidar com essa expressão artística pode ser desafiador. A complexidade emocional é uma constante que afeta minha jornada...";
                                break;
                        }
                        break;
                    case 1:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "Lidar com essa confusão é um desafio constante, como se os anseios e sombras se entrelaçassem, criando uma paisagem difícil de navegar...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Tento compreender e lidar com a confusão das cores, mas a complexidade muitas vezes torna a navegação emocional desafiadora...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A confusão das cores e a indefinição das emoções são uma constante que molda minha jornada, tornando-a mais complexa...";
                                break;
                        }
                        break;
                    case 2:
                        switch ((ProblemType)corruption)
                        {
                            case ProblemType.DEPRESSION:
                                messageText.text = "É uma experiência íntima, como se a complexidade emocional contribuísse para um isolamento interno, tornando difícil compartilhar essa expressão artística...";
                                break;
                            case ProblemType.BURNOUT:
                                messageText.text = "Compartilho em alguns momentos, mas a comunicação emocional muitas vezes se torna desafiadora, contribuindo para a complexidade interna...";
                                break;
                            case ProblemType.ANXIETY:
                                messageText.text = "A experiência emocional é única, e embora compartilhe algumas nuances, a complexidade persiste...";
                                break;
                        }
                        break;
                }
                break;
        }
    }

    public void NextDialogue()
    {
        if (_canAsk) ReturnQuestions();
        else
        {
            if (messageText.text == sentences[index])
            {
                if (index < sentences.Length - 1)
                {
                    index++;
                    messageText.text = string.Empty;
                    StartCoroutine(TypingSentence());
                }
                else
                {
                    index = 0;
                    messageText.text = string.Empty;
                    dialogueObj.SetActive(false);
                    _player.canMove = true;
                }
            }
            else
            {
                index = 0;
                messageText.text = string.Empty;
                dialogueObj.SetActive(false);
                _player.canMove = true;
            }
        }
    }

    IEnumerator TypingSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
