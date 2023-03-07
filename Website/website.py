#website.py
''' Contributors: Cole Sarno
    Program Description: The python file for the website
    Course: CMSC 495 6383 
    Class: Current Trends and Projects in Computer Science
    Created: 02/07/2023
    Last Updated: 03/07/2023
'''

#Imports os and sys for file processing
import os
import sys
#Imports datetime for 
from datetime import datetime
from flask import Flask
from flask import render_template
from flask import current_app as app
from flask import request
from flask import flash
#For reading csv file
import pandas as pd
import numpy as np

#Global variable for adding information to the bug_report.csv file
bug_report = []

app = Flask(__name__)
#Randomizes the secret key for flash
app.secret_key = os.urandom(12)
@app.route('/')
def index():
    '''
    Redirects the user to the homepage with the home function
    '''
    return home()
@app.route('/home/')
def home():
    '''
    Brings the user to the homepage
    '''
    return render_template('home.html')
@app.route('/about/')
def about():
    '''
    Brings user to the about page
    '''
    return render_template('about.html')
@app.route('/game_updates/')
def game_updates(update_ls=None):
    '''
    Brings user to the game updates page
    '''
    #Calls the read_update_file function, which returns a boolean, string, and 2D list
    success, err_msg, ls = read_update_file()
    #Sets the webpage variable update_ls to the contents of the list
    update_ls = reversed(ls)
    #Checks if the read_update_file function returns FALSE
    if not success:
        #Sends the error message string through flash
        flash(err_msg)
    #Renders the template, sending the update_ls variable
    return render_template('game_updates.html', update_ls=update_ls)
@app.route('/report_bug/', methods=['GET','POST'])
def report_bug(success_msg=None):
    '''
    Brings user to the report bug page
    '''
    #Checks if the user has clicked the submit button on the form
    if request.method == "POST":
        #Gathers the current datetime and splits it up into date and time
        date_time = datetime.now()
        date = date_time.strftime("%A %B %d %Y")
        time = date_time.strftime("%I:%M:%S PM")
        #Gathers the user's IP address
        ip_address = request.remote_addr
        #Gets the bug description info from the form textarea
        bug_desc = request.form["bug_desc"]
        #Adds the date, time, IP address, bug description to the bug_report list
        bug_report.append(date)
        bug_report.append(time)
        bug_report.append(ip_address)
        bug_report.append(bug_desc)
        #Tries to write a bug report by passing the bug_report list
        success, err_msg = write_bug_report(bug_report)
        #Checks if the write_bug_report function call was successful
        if success:
            success_msg = "Your bug has been successfully reported!"
        #If success returns FALSE, sends the error message over flask
        if not success:
            flash(err_msg)
    # Renders the template, sending the success_msg variable
    return render_template('report_bug.html', success_msg=success_msg)
@app.route('/web_player/')
def web_player():
    '''
    Brings user to the web player page
    '''
    return render_template('web_player.html')
def read_update_file():
    '''
    Reads data from the game_updates.csv file
    Returns:
        success (bool): A boolean that determines the success of the function,
            is FALSE by default
        err_msg (string): A string error message describing any issues found,
            will be an empty string if and only if success is TRUE
        ls (string[][]): A 2D array of strings that stores the data for each line
            within an array, which is then stored within a larger array of each line
            data structure should look like:
            [[1a,1b,1c],[2a,2b,2c],[3a,3b,3c]...]
            where 1,2,3 are line numbers and a,b,c are variables on each line
    '''
    #Initializes error message and list to empty variables and success to FALSE
    err_msg = ""
    ls = []
    success = False
    #Sets the update path to the system path to the game_updates.csv file
    update_path = os.path.join(sys.path[0] + "\\" + "game_updates.csv")
    #Checks if the update path does NOT exist
    if not os.path.exists(update_path):
        #Sets an error message
        err_msg = "Error: Could not access Game Updates"
    #Else, the update path does exist
    else:
        #Tries to open the game_updates.csv file in read mode as variable read_file
        with open(update_path, "r", encoding='utf-8') as read_file:
            #Uses pandas(pd) to read the data from the csv read_file
            data = pd.read_csv(read_file)
            #Uses numpy(np) to put the data for each line into an array
            #The list(ls) is a 2D array split up into each line
            ls = np.array(data)
            #Checks if list(ls) is empty
            if len(ls) == 0:
                #Sets an error message
                err_msg = "Error: No Game Updates were Found"
            #Else, the list(ls) has been successfully populated
            else:
                success = True
    #Returns the success(bool), error message(string), and list(2D array)
    return success, err_msg, ls
def write_bug_report(bug_report):
    '''
    Reads data from the game_updates.csv file
    Parameters:
        bug_report(ls[]): A global variable list for storing bug report information
    Returns:
        success(bool): A boolean that determines the success of the function,
            is FALSE by default
        err_msg(string): A string error message describing any issues found,
            will be an empty string if and only if success is TRUE
    '''
    # Initializes error message to an empty string and success to FALSE
    err_msg = ""
    success = False
    # Sets the report path to the system path to the bug_report.csv file
    report_path = os.path.join(sys.path[0] + "\\" + "bug_report.csv")
    #Tries to open and write to the file, catches FileNotFoundError
    try:
        #Tries to open the game_updates.csv file in append mode as variable write_file
        with open(report_path, "a", encoding='utf-8') as write_file:
            #Iterates through each item(i) in the bug_report global list
            for i in bug_report:
                #Writes each item(i) to a line in the bug_report.csv file
                write_file.writelines(i + ",")
            #Creates a new line after each item has been written to the line
            write_file.writelines("\n")
        #Sets the success to TRUE
        success = True
    #If the file is not found, excepts FileNotFoundError as e
    except FileNotFoundError as e:
        #Prints the error to the console
        print(e)
        #Sets an error message
        err_msg = "Error: Cannot report bugs at this time"
    #Returns the success(bool) and error message(string)
    return success, err_msg
#Used to run the main method of the application when this file is run
if __name__ == "__main__":
    app.run()
