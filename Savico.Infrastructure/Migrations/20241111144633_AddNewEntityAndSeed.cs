using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Savico.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tips",
                columns: new[] { "Id", "Content" },
                values: new object[,]
                {
                    { 1, "Buy generic bread, save for your dream yacht." },
                    { 2, "Don't let your spending get out of control—unless it's a coupon you found in your pocket!" },
                    { 3, "Budgeting is like dieting. You can’t keep eating chips if you want to fit in your financial goals." },
                    { 4, "Save your change. Someday, it might be worth a lot... or at least enough for a nice coffee." },
                    { 5, "It’s not ‘expensive coffee,’ it’s ‘investment in happiness’—unless your budget says otherwise." },
                    { 6, "Don’t stress about your budget—just make sure your credit card doesn’t get any ideas." },
                    { 7, "Saving money is like washing your car—you feel like you're not getting anywhere, but it’s worth it in the end." },
                    { 8, "Don’t spend your money too fast—unless it's on ice cream, because ice cream is always worth it." },
                    { 9, "Financial freedom is just a few small sacrifices away... starting with not buying that extra pair of shoes." },
                    { 10, "Be patient and persistent in achieving your financial goals." },
                    { 11, "Always compare prices before making significant purchases." },
                    { 12, "Save a portion of any raise or bonus you receive." },
                    { 13, "Start budgeting and keep track of all income and expenses." },
                    { 14, "Review your budget regularly to stay on track." },
                    { 15, "Always plan for the future, not just for the present." },
                    { 16, "Try to build an emergency fund for unexpected expenses." },
                    { 17, "Set realistic financial goals and work towards them consistently." },
                    { 18, "Track your spending to know where your money is going." },
                    { 19, "Put your savings in a jar... and then hide it from yourself so you can’t spend it!" },
                    { 20, "Save money by spending it on things that’ll last forever, like a good pair of socks (because socks don’t expire)." },
                    { 21, "If you save enough money, you can one day buy that thing you don’t need. Think of the possibilities!" },
                    { 22, "You can’t buy happiness, but you can buy a savings account, and that’s pretty much the same thing." },
                    { 23, "Turn your coffee habit into a savings plan: swap your latte for instant coffee, then use the savings to buy something ridiculous!" },
                    { 24, "Save on heating bills by wearing all your clothes at once—fashion is subjective, right?" },
                    { 25, "Instead of spending money on fancy things, spend time with people who think free stuff is cool." },
                    { 26, "Save on heating bills by wearing all your clothes at once—fashion is subjective, right?" },
                    { 27, "Cut costs by cutting back on things you don’t really need, like your subscription to the ‘I like pizza’ fan club." },
                    { 28, "Put your savings in a piggy bank and then give it a name. If you bond with it, you’ll resist the urge to break it open." },
                    { 29, "The best way to save money? Stop buying things that seem like a good idea at 2 a.m. after too much coffee." },
                    { 30, "Saving money is like a treasure hunt. The more you save, the more you realize it was worth the effort—and the loot is much cooler than that impulse buy." },
                    { 31, "To save more, stop buying things you don’t need and start selling things you forgot you bought." },
                    { 32, "Remember: Money can’t buy happiness, but it can buy a ton of snacks for your movie marathon. And that’s basically the same thing." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tips");
        }
    }
}
