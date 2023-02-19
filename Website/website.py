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

from datetime import datetime
from flask import Flask
from flask import render_template
from flask import current_app as app
from flask import request
from flask import flash

bug_report = []

app = Flask(__name__)
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
def game_updates():
    '''
    Brings user to the game updates page
    '''
    return render_template('game_updates.html')
@app.route('/report_bug/', methods=['GET','POST'])
def report_bug():
    '''
    Brings user to the report bug page
    '''
    if request.method == "POST":
        date = request.form["date"]
        bug_desc = request.form["bug_desc"]
        bug = date + bug_desc
        bug_report.append(bug)
    return render_template('report_bug.html')

@app.route('/web_player/')
def web_player():
    '''
<<<<<<< HEAD
    Brings user to the web player page
=======
    Brings user to the game web player
    '''
    return render_template('web_player.html')

if __name__ == "__main__":
    app.run()
