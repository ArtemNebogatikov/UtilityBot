using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Services;
using UtilityBot.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace UtilityBot.Controllers
{
    class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Текст", $"text" ),
                        InlineKeyboardButton.WithCallbackData($"Числа", $"number" )
                    });
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Этот бот может посчитать количество символов в фразе </b> {Environment.NewLine}"
                         +
                        $"{Environment.NewLine} Так же он может посчитать сумму чисел написанных через пробел {Environment.NewLine}"
                        + $"{Environment.NewLine} Если нужно посчитать длину выбери текст, если сумму то числа{Environment.NewLine}",
                        cancellationToken: ct, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    string userActionCode = _memoryStorage.GetSession(message.Chat.Id).ActionCode;
                    switch (userActionCode)
                    {
                        case "text":
                            var length = CountText.LengthText(message.Text);
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина текста {length} символов", cancellationToken: ct);
                            break;
                        case "number": 
                            var count = CountNumber.Count(message.Text);
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел {count}", cancellationToken: ct);
                            break;
                    }
                        
                    break;
            }
        }
    }
}
