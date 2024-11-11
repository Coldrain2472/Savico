namespace Savico.Infrastructure.Data.DataSeeding.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Savico.Infrastructure.Data.Models;

    public class TipConfiguration : IEntityTypeConfiguration<Tip>
    {
        public void Configure(EntityTypeBuilder<Tip> builder)
        {
            builder.HasData(this.SeedTips());
        }

        private IEnumerable<Tip> SeedTips()
        {
            IEnumerable<Tip> tips = new List<Tip>()
            {
                new Tip(){Id = 1, Content = "Buy generic bread, save for your dream yacht."},
                new Tip(){Id = 2, Content = "Don't let your spending get out of control—unless it's a coupon you found in your pocket!"},
                new Tip(){Id = 3, Content = "Budgeting is like dieting. You can’t keep eating chips if you want to fit in your financial goals."},
                new Tip(){Id = 4,  Content = "Save your change. Someday, it might be worth a lot... or at least enough for a nice coffee."},
                new Tip(){Id = 5, Content = "It’s not ‘expensive coffee,’ it’s ‘investment in happiness’—unless your budget says otherwise."},
                new Tip(){Id = 6,  Content = "Don’t stress about your budget—just make sure your credit card doesn’t get any ideas."},
                new Tip(){Id = 7, Content = "Saving money is like washing your car—you feel like you're not getting anywhere, but it’s worth it in the end."},
                new Tip(){Id = 8,  Content = "Don’t spend your money too fast—unless it's on ice cream, because ice cream is always worth it."},
                new Tip(){Id = 9, Content = "Financial freedom is just a few small sacrifices away... starting with not buying that extra pair of shoes."},
                new Tip(){Id = 10,  Content = "Be patient and persistent in achieving your financial goals."},
                new Tip(){Id = 11,  Content = "Always compare prices before making significant purchases."},
                new Tip(){Id = 12, Content = "Save a portion of any raise or bonus you receive."},
                new Tip(){Id = 13,  Content = "Start budgeting and keep track of all income and expenses."},
                new Tip(){Id = 14, Content = "Review your budget regularly to stay on track."},
                new Tip(){Id = 15, Content = "Always plan for the future, not just for the present."},
                new Tip(){Id = 16, Content = "Try to build an emergency fund for unexpected expenses."},
                new Tip(){Id = 17, Content = "Set realistic financial goals and work towards them consistently."},
                new Tip(){Id = 18, Content = "Track your spending to know where your money is going."},
                new Tip(){Id = 19, Content = "Put your savings in a jar... and then hide it from yourself so you can’t spend it!"},
                new Tip(){Id = 20, Content = "Save money by spending it on things that’ll last forever, like a good pair of socks (because socks don’t expire)."},
                new Tip(){Id = 21, Content = "If you save enough money, you can one day buy that thing you don’t need. Think of the possibilities!"},
                new Tip(){Id = 22, Content = "You can’t buy happiness, but you can buy a savings account, and that’s pretty much the same thing."},
                new Tip(){Id = 23, Content = "Turn your coffee habit into a savings plan: swap your latte for instant coffee, then use the savings to buy something ridiculous!"},
                new Tip(){Id = 24, Content = "Save on heating bills by wearing all your clothes at once—fashion is subjective, right?"},
                new Tip(){Id = 25, Content = "Instead of spending money on fancy things, spend time with people who think free stuff is cool."},
                new Tip() {Id = 26, Content = "Save on heating bills by wearing all your clothes at once—fashion is subjective, right?"},
                new Tip() {Id = 27, Content = "Cut costs by cutting back on things you don’t really need, like your subscription to the ‘I like pizza’ fan club."},
                new Tip() {Id = 28, Content = "Put your savings in a piggy bank and then give it a name. If you bond with it, you’ll resist the urge to break it open."},
                new Tip() {Id = 29, Content = "The best way to save money? Stop buying things that seem like a good idea at 2 a.m. after too much coffee."},
                new Tip() {Id = 30, Content = "Saving money is like a treasure hunt. The more you save, the more you realize it was worth the effort—and the loot is much cooler than that impulse buy."},
                new Tip() {Id = 31, Content = "To save more, stop buying things you don’t need and start selling things you forgot you bought."},
                new Tip() {Id = 32, Content = "Remember: Money can’t buy happiness, but it can buy a ton of snacks for your movie marathon. And that’s basically the same thing."}
            };

            return tips;
        }
    }
}
