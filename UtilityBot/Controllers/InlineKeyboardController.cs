using UtilityBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UtilityBot.Controllers
{
    class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if(callbackQuery?.Data == null) 
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).ActionCode = callbackQuery.Data;

            string actionText = callbackQuery.Data switch
            {
                "text" => "Считаем длинну строки",
                "number" => "Считаем сумму чисел",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, actionText, cancellationToken: ct);
        }
    }
}
