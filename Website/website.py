#website.py
''' Contribitors: Cole Sarno,
    Program Description: The python file for the website
    Course: CMSC 495 6383 
    Class: Current Trends and Projects in Computer Science
    Created: 02/07/2023
    Last Updated: 02/12/2023
'''
#New imports re, string
import re
import string
#Imports os and sys for file processing
import os
import sys
from datetime import datetime
from flask import Flask
from flask import render_template
from flask import current_app as app
from flask import request
from flask import flash
#For reading csv file
import pandas as pd
import numpy as np

bug_report = []

app = Flask(__name__)
app.secret_key = 'secret_key'
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
    success, err_msg, ls = read_update_file()
    update_ls = reversed(ls)
    if not success:
        flash(err_msg)
    return render_template('game_updates.html', update_ls=update_ls)
@app.route('/report_bug/', methods=['GET','POST'])
def report_bug(success_msg=None):
    '''
    Brings user to the report bug page
    '''
    if request.method == "POST":
        date_time = datetime.now()
        date = date_time.strftime("%A %B %d %Y")
        time = date_time.strftime("%I:%M:%S PM")
        ip_address = request.remote_addr
        bug_desc = request.form["bug_desc"]
        bug_report.append(date)
        bug_report.append(time)
        bug_report.append(ip_address)
        bug_report.append(bug_desc)
        success, err_msg = write_bug_report(bug_report)
        if success:
            success_msg = "Your bug has been successfully reported!"
        if not success:
            flash(err_msg)
    return render_template('report_bug.html', success_msg=success_msg)

@app.route('/web_player/')
def web_player():
    '''
    Brings user to the web player page
    '''
    return render_template('web_player.html')

def read_update_file():
    err_msg = ""
    ls = []
    success = False
    update_path = os.path.join(sys.path[0] + "\\" + "game_updates.csv")
    if not os.path.exists(update_path):
        success = False
        err_msg = "Error: Could not access Game Updates"
    else:
        with open(update_path, "r", encoding='utf-8') as read_file:
            data = pd.read_csv(read_file)
            ls = np.array(data)
            if len(ls) == 0:
                err_msg = "Error: No Game Updates were Found"
            else:
                success = True
    return success, err_msg, ls
def write_bug_report(bug_report):
    success = False
    err_msg = ""
    report_path = os.path.join(sys.path[0] + "\\" + "bug_report.csv")
    try:
        with open(report_path, "a", encoding='utf-8') as write_file:
            for i in bug_report:
                write_file.writelines(i + ",")
            write_file.writelines("\n")
        success = True
    except FileNotFoundError as e:
        print(e)
        err_msg = "Error: Cannot report bugs at this time"
    return success, err_msg
    
if __name__ == "__main__":
    app.run()
