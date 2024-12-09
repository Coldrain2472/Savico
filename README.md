# :moneybag: Savico - Your Budget Management Web App :dart: 

**Savico** is a simple app designed to make managing your finances easier. With its help, you can track your income, expenses, and set financial goals, while letting the app handle the calculations for your available budget. Whether you’re saving for a vacation or your dream PC, Savico helps you make your financial goals a reality. 

It’s that simple: input your incomes, expenses, and goals, and let Savico calculate your budget and provide useful, and sometimes silly, tips!

# 📋 Features:

### 🏷️ Roles:

**👤 User:**
- Insert and manage their incomes and expenses, with automatic budget calculations.
- Create financial goals and contribute to them, with contributions deducted from their available budget.
- Generate detailed reports (weekly, monthly, yearly) to review incomes and expenses sorted by date.

**🛠️ Admin:**
- Access a dedicated Admin Panel for managing Users.
- Deactivate or reactivate User accounts (Ban or Remove Ban feature).
- Promote Users to Admin or demote Admins to Users.
- View active and inactive User statistics.

### 🔑 Core Functionality:
- **Authenticated User Access**: Access to home page, all income pages, all expense pages, all report pages, and all goal pages.
- **Guest Access**: Limited to registration and login pages.
- **Privacy**: Users can only see their own incomes, expenses, goals, and reports.
- **User Registration and Login**: Secure account management with roles for "User" and "Admin".
- **Income Management**: Add, edit, and track multiple sources of income.
- **Expense Management**: Record and categorize expenses with filtering options by date and amount.
- **Automatic Budget Calculation**: Calculates the available budget based on income and expenses.
- **Goal Tracking**: Set financial goals and contribute to them, with real-time budget updates.
- **Reports**: Generate and view monthly or yearly reports for income and expenses.

### 🏢 Administrator Area:
- **User Management**: View a list of registered Users with basic details.
- **Account Control**: Deactivate or reactivate User accounts.
- **Role Management**: Assign or modify User roles (e.g., promoting a User to an Admin).
- **Dashboard**: Monitor total Users and account statuses.

### 📊 User Dashboard:
**Financial Metrics:**
- Total Income.
- Total Expenses.
- Available Budget.

📈 **Visualizations:**
- A small, hidden (in the Home Index View, click on the Total Expenses card) and interactive pie chart providing a quick financial overview (powered by Chart.js and HTML5 Canvas).

# 🔐 Security:
- **CSRF Protection**: Implemented via `@Html.AntiForgeryToken()` in the views and `[ValidateAntiForgeryToken]` in the controllers.
- **Input Validation**: Server-side and client-side validation for all forms.
- **Data Protection**: Soft delete functionality for sensitive data.
- **Secure Authentication**: Built-in ASP.NET Core Identity for user authentication.

# 💻 Technologies
- **Backend**: ASP.NET Core with MVC pattern.
- **Frontend**: Razor Pages with Bootstrap Theme SB Admin 2.
- **Database**: MS SQL Server
- **Charting**: Chart.js (utilizing HTML5 canvas).
- **Testing**: NUnit

# 📄 License:
This project is licensed under the Apache-2.0 license - see the LICENSE.md file for details.

## 🚀 Future Ideas:
- **Admin User Deletion**: Allow Admins to permanently delete User accounts (currently, only deactivation is possible with the Ban feature).
- **Recurring Expenses/Income**: Add functionality for recurring incomes and expenses (e.g., monthly subscriptions, salary).
- **Multi-Currency Support**: Enable users to set their preferred currency and convert between currencies based on real-time exchange rates.
- **Notifications**: Provide notifications or alerts for users when they’re approaching or exceeding their budget limits.
- **Advanced Reporting**: Implement more advanced reporting options like trend analysis and forecast based on user’s spending habits.
  
# :construction: THIS PROJECT IS STILL UNDER CONSTRUCTION :construction:
