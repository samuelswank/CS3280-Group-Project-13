# CS3280-Group-Project-13

## Group Project Prototype (May not be turned in late)

The first part due for the group project is a prototype of the final project.  The full project requirements can be viewed in the next section.  Please make sure to read all requirements for the Final Group Project.  This part of the assignment is to get each group member engaged and to assign each role for the assignment.  The final project must be done in WPF.  The prototype will be turned in by one member of the group. Please work with your group for any code questions.  I should only be used if all group members can’t solve an issue.

The group project will be broken up so that each member is responsible for everything for a single Window.  So, for a 3-person team, one person will do everything for the Main Window, another person does the Search Window, and the last does the Edit Items Window.

The prototype of the group project will consist of a preliminary design of the UI, how each person’s code will interface with other code, and the SQL for the assignment in a class.  Make sure to read the final group project requirements to fully understand what the UI will look like and how the program will work when completed.

The Visual Studio project will be created and 3 folders will be added to the project.  The folders will be called “Search”, “Main”, and “Items”.  Inside these folders, each member will put their respective code files. 

For the “Search” folder, there should be a XAML file for the UI called “wndSearch.xaml”, another file named “clsSearchSQL” which contains all SQL statements for the Search Window, and the last file should be “clsSearchLogic” which will contain all business logic for the Search Window.

For the “Main” folder, there should be a XAML file for the UI called “wndMain.xaml”, another file named “clsMainSQL” which contains all SQL statements for the Main Window, and the last file should be “clsMainLogic” which will contain all business logic for the Main Window.

For the “Items” folder, there should be a XAML file for the UI called “wndItems.xaml”, another file named “clsItemsSQL” which contains all SQL statements for the Items Window, and the last file should be “clsItemsLogic” which will contain all business logic for the Items Window.

### GUI

All of the screens should be created with all controls needed to complete the requirements.  For instance, on the search screen, there should be 3 drop down boxes for selection, a DataGrid, and select and cancel buttons.  Once each screen has been created the flow of the program needs to be completed.  So, for example, on the main form, there should be a menu with the selection of “Search for Invoice” that when clicked should open the search window, then when the user clicks the “Select” or “Cancel” buttons the search window should close and the main form get focus.

### Interfaces

This part of the assignment is to put together a plan on how each screen will pass the data to the other screens.  This will be done by putting the appropriate comments in the sections of stubbed out code to explain how the data will be passed around.  This will get you thinking about how each screen will interface with the others. 

Search Window – Must have comments about how the Invoice ID will be stored when the user selects an invoice, and how the main window will access that variable.

Main Window – Must have comments about how after the search window is opened, how the main window will extract the Invoice ID from the search window.  Also needs another comment that describes how the items combo box will be refreshed if any items are modified/added on the items window.

Items Window – Must have a comment about how it will set a local variable that the main window may access to determine if any items have been modified/updated.

### SQL

This part of the assignment is to create a class for each window that contains the SQL needed for that window.  Each SQL class will be nothing but methods that contain different statements of SQL.  Make sure to create SQL statements that will help in meeting all requirements that use the database.  So, SQL statements needed will be to select different types of data on each window, to update/insert data on each form.  Use Microsoft Access to run the queries ahead of time to make sure the queries give you the expected data and work correctly.  Your SQL statements should be tested and working.  Below is an example of a class/method that should be used as a guide for your code.
 
    class clsSQL

    {

        /// <summary>

        /// This SQL gets all data on an invoice for a given InvoiceID.

        /// </summary>

        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>

        /// <returns>All data for the given invoice.</returns>

        public string SelectInvoiceData(string sInvoiceID)

        {

            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;

        }

    }

NOTE: Only one person should submit the protype assignment with all members’ name in canvas.


### Commonly Missed Items

- Didn't meet the following requirement - "The Visual Studio project will be created and 3 folders will be added to the project.  The folders will be called “Search”, “Main”, and “Items”.  Inside these folders, each member will put their respective code files."

- Didn't meet the following requirement - "For example, inside the “Search” folder, there should be a XAML file for the UI called something like “wndSearch.xaml”, another file named something like “clsSearchSQL” which contains all SQL statements for the Search Window, and the last file should be something like “clsSearchLogic” which will contain all business logic for the Search Window."

- Didn't meet the following requirement - "Once each screen has been created the flow of the program needs to be completed.  So, for example, on the main form, there should be a menu with the selection of “Search for Invoice” that when clicked should open the search window, then when the user clicks the “Select” or “Cancel” buttons the search window should close and the main form get focus."

- Didn't meet the following requirement - "This will be done by putting the appropriate comments in the sections of stubbed out code to explain how the data will be passed around.  This will get you thinking about how each screen will interface with the others.  So for example, on the search screen, behind the button click event for the “Select” button, there should be a detailed comment about how the selected InvoiceID will be passed back to the main form."  Also should be comments explaining how the main window will be refreshed when the Items window is closed.

 

#### Main

-----------

- Didn't meet the requirement "Main window will have a menu (at the top, use a menu control) that will have two functionalities.  The first will allow the user to update a def table that contains the items.  The next will be to open a search screen used to search for invoices."

- Main window class clsMainSQL does not have all of the SQL needed for the window.  All SQL was given in the Database Help document.

- Main window class clsMainSQL has business logic in it, the requirements were to create SQL classes that had just the SQL statements and that's it.

- Main window needs comments to talk about how it will interact with the other windows to get the selected invoice or know when to refresh the items.

- Main window, when the Item's Window is closed needs to have comments talking about refreshing the combo boxes with the items in it, in case they got updated.

 

#### Search

----------

- Search window class clsSearchSQL does not have all of the SQL needed for the window.  All SQL was given in the Database Help document.

- Search window class clsSearchSQL has business logic in it, the requirements were to create SQL classes that had just the SQL statements and that's it.

- Search window should have a comment about when the invoice is selected, the Invoice ID is saved in a local variable that the main window can access.

 

#### Item

---------

- Item window class clsItemSQL does not have all of the SQL needed for the window.  All SQL was given in the Database Help document.

- Items window class clsItemsSQL has business logic in it, the requirements were to create SQL classes that had just the SQL statements and that's it.

 

For a .NET core app, you will need to do the following to get it to work.

Right click the "Dependencies" section under the "Solution Explorer" tab.
Select "Manage NuGet Packages".
Click the "Browse" option at the top right.
Search for "OleDB", then select "System.Data.OleDb".
On the right click the "Install" button, then click the "Ok" button.
Recompile your app and it should work.
 

I created the following video help you with the assignment.  I would recommend trying to solve the assignment without watching it first, as it might influence how you would solve the problem.  I created it so that if you get lost or need direction, this video will show you a path forward.

## Group Project

Create a Windows WPF program that can be used as in invoice system for a small business.  The type of business is up to you.  Examples of a business would be a Supplement Store, Jewelry Store, Shoe Store, Equipment Rental Store, etc.  A Microsoft Access database should be used as the backend database to store the invoice data.  The group assignment will be turned in by one member of the group. Please work with your group for any code questions.  I should only be used if all group members can’t solve an issue.

The group project will be broken up so that each member is responsible for everything for a single Window.  So, for a 3-person team, one person will do everything for the Main Window, another person does the Search Window, and the last does the Edit Items Window.

The Visual Studio project will be created and 3 folders will be added to the project.  The folders will be called “Search”, “Main”, and “Items”.  Inside these folders, each member will put their respective code files. 

For the “Search” folder, there should be a XAML file for the UI called “wndSearch.xaml”, another file named “clsSearchSQL” which contains all SQL statements for the Search Window, and the last file should be “clsSearchLogic” which will contain all business logic for the Search Window.

For the “Main” folder, there should be a XAML file for the UI called “wndMain.xaml”, another file named “clsMainSQL” which contains all SQL statements for the Main Window, and the last file should be “clsMainLogic” which will contain all business logic for the Main Window.

For the “Items” folder, there should be a XAML file for the UI called “wndItems.xaml”, another file named “clsItemsSQL” which contains all SQL statements for the Items Window, and the last file should be “clsItemsLogic” which will contain all business logic for the Items Window.

### Main window

The main window should allow the user to create new invoices or edit existing invoices.  There should be just one window for all functionality of the main window.  So, the main window will NOT open other windows to add/edit invoices.  It will also have a menu (at the top, use a menu control) that will have two functionalities.  The first will allow the user to update a def table that contains the items.  The next will be to open a search screen used to search for invoices.

If a new invoice is created the user may enter data pertaining to that invoice.  Since an auto-generated number is used in the database for the invoice number, when a user creates a new invoice, just display “TBD” for the Invoice Number.  An invoice date will also be assigned by the user.  Next different items will be entered by the user.  The items will be selected from a drop-down box and the cost for that item will be put into a read only textbox.  This will be the default cost of an item. Once the item is selected, the user can add the item.  As many items as needed should be able to be added.  All items entered should be displayed for viewing in a list (something like a DataGrid).  Items may be deleted from the list.  A running total of the cost of all items should be displayed as items are entered or deleted.

Once all the items are entered the user can save the invoice.  Once the Invoice is saved, query the max invoice number from the database, to display for the invoice number (for our project, this will work, since the last inserted invoice, will be the max).  This will lock the data in the invoice for viewing only.  From here the user may choose to Edit the Invoice. 

### Search Screen

The user also needs to be able to search for invoices, which will be a choice from the menu.  On the search screen, all invoices should be displayed in a list (like a DataGrid) for the user to select.  The user may limit the invoices displayed by choosing an Invoice Number from a drop down, selecting an invoice date, or selecting the total charge from a drop-down box.  The total charges in the drop-down box should be the unique set of total charges sorted from smallest to largest.  When a limiting item is selected, the list should only reflect those invoices that match the criteria.  So, the user should be able to select a date and a total charge, and only invoices that match both will be displayed.  A clear selection button should reset the form to its initial state.  Once an invoice is selected the user will click a “Select Invoice” button, which will close the search form and open the selected invoice up for viewing on the main screen.  From there the user may choose to Edit the invoice.

### Items Window

The last form needed is a form to update the def table which contains all the items for the business.  This form can be accessed through the menu and only when an invoice is not being edited or a new invoice is being entered.  This form will list all the items in a list (like a DataGrid).  The items will consist of a code, cost, and description.  From here the user can add new items, edit existing items, or delete existing items.  If the user tries to delete an item that is on an invoice, don’t allow the user to do so.  Instead warn them with a message that tells the user which invoices that item is used on.  When an item is updated, the code must not be allowed to be updated because it is the primary key, only the description and cost may be updated.  When the user closes the update def table form, make sure to update the drop-down box as to reflect any changes made by the user.  Also update the current invoice because its item name might have been updated.

Since this is the final project all lessons learned throughout the course should be used and implemented.  Don’t forget to abstract your business logic into classes and keep you UI code clean.  Make sure to test all user inputs so your program doesn’t crash, have another group member test your code thoroughly.  All methods should handle exceptions.  Since this a WPF application you should use styles for your applications.  At a minimum, a theme should be applied to the application, such as one talked about in the Microsoft Blend lecture.  Visual properties shouldn’t be hard coded into controls, they should be put into styles and applied to controls.

### Guidelines

- Project Submission: you must turn it in by midnight on the due date.

- Only one person should submit the assignment with all members’ name in the canvas submission.

### Common Mistakes

- Student's didn't unit test each other’s code

- Forgot to use styles

- Business logic behind the UI

- Validate all user input

 

### Tips

- Run each other’s code to test for bugs

- Look at each other’s code

- Verify all requirements

- Run through the presentation together

- Break up the project so that each member is responsible for everything for a single Window.

 

### DataGrid Help

Keeps thing simple.
On the edit item screen, instead of using the DataGrid like an excel spreadsheet, just show each selected item in textboxes next to the DataGrid
To get rid of the extra row on the bottom of the DataGrid set the following property:
CanUserAddRows="False"
 

### Microsoft Access Help

To view data in a table: In the “Tables” menu on the left-hand side of the screen, double click the data you wish to the view the data for.

To create and test queries: Click the “Create” ribbon item, click the “Query Design” button.  This brings up the ability to create queries in a designer, if you know how to work this go ahead, if you want to write queries manually and test them, then close the “Show Table” window, then click the “SQL View” button at the top left corner, select “SQL View”.  Now create your SQL statements, then click the “Run” button on the ribbon.

### Commonly Missed Items

#### Search

- Requirement to “On the search screen all invoices should be displayed in a list”.

- On the search screen the combo boxes should only have distinct values, not duplicates, and should be ordered so users can find the values they need.

- Search window has business logic behind the UI.

- Search window, the date and total charge should be combined as a filter if both are selected.

- Requirement to have a clear selection button.

- Each time the search window is opened it should refresh invoices and combo boxes because an invoice could have been updated.

- Requirement to theme the application.

- All Classes, Attributes, and Methods need XML comments.

- Try catch blocks not implemented correctly.

- All methods need a try catch block

- All business logic has been put into the classes that are just supposed to be SQL statements.

#### Main

- I can only add 1 type of item to an invoice.  I should be able to select an item and click the Add item button multiple time for the same item.

- When I create a new invoice, the total charge is always zero in the search screen so it is not being saved to the db.

- Main window has business logic behind the UI.

- When creating an invoice, the Invoice number is not given

- Requirement to have a menu control to get to the search and edit screens.

- After an invoice is selected it should be displayed in read only mode and an edit button needs to be clicked.

- After a new invoice is created and saved, it should be displayed in read only mode.

- Main window items need to be refreshed after items have been updated/deleted/added on the items window.

- Shouldn't be able to save an invoice without entering a date.

- Didn't meet the requirement "An invoice date will also be assigned by the user."

- After an invoice is searched for and selected, the main window should display it in a read only mode, then an edit button needs to be clicked to edit it.

#### Items

- When deleting an item on the def table form, a requirement says “Instead warn them with a message that tells the user which invoices that item is used on.”.  Your message doesn’t tell which invoices it is on.

- When deleting an item on the def table form, if the item exists on invoices, then distinct invoices should be displayed.

- Need to validate user input on the update def table window.  If I enter very long strings the program raises an exception.

Def table window has business logic behind the UI.
