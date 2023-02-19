#website.py
''' Contribitors: Cole Sarno, 
    Program Description: The python file for the website
    Course: CMSC 495 6383 
    Class: Current Trends and Projects in Computer Science
    Created: 02/07/2023
    Last Updated: 02/12/2023
'''
from datetime import datetime
from flask import Flask
from flask import render_template
from flask import current_app as app

app = Flask(__name__)

gameUpdateList = [['Author', 'Date', 'Description'], ['Author', 'Date', 'Description'], ['Author', 'Date', 'Description']]

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
    return render_template('game_updates.html', updateList=gameUpdateList)
@app.route('/report_bug')
def report_bug():
    '''
    Brings user to the report bug page
    '''
    return render_template('report_bug.html')

if __name__ == "__main__":
    app.run()
