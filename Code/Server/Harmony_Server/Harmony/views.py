from datetime import datetime
from .models import File
from .forms import FileForm
import os
from urllib.parse import parse_qs
from django.conf import settings
from django.http import HttpResponse, request
from django.shortcuts import render, redirect
import pymysql
import sqlite3
from django.views.decorators.csrf import csrf_exempt
from django.utils import timezone
# 회원가입 버튼 눌릴시 signup.html을 response (GET method), url : /accounts/signup
def signup_pg(request):
    if request.method == "GET":
        return render(request, 'Harmony/signup.html')


# 회원가입 제출 버튼을 눌렀을때, user info를 POST로 전달 받아서 DB에 저장
@csrf_exempt
def signup(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    if request.method == "POST":
        id = request.POST['ID']
        pw = request.POST['PW']
        instrument = request.POST.getlist("instrument[]")
        drum = "0"
        keyboard = "0"
        launchpad = "0"

        if "drum" in instrument:
            drum = "1"
        if "keyboard" in instrument:
            keyboard = "1"
        if "launchpad" in instrument:
            launchpad = "1"

        query1 = ("""
                          SELECT m.ID, m.PW
                          FROM HarmonyDataBase.Member m
                          WHERE m.ID = %s;"""
                  )
        curs.execute(query1, (id,))  # mysql로 부터 입력한 id와 일치하는 회원정보 받아오기
        dict1 = curs.fetchall()  # 전부

        # 일치하는 id 없다면 회원가입해도 좋다
        if len(dict1) == 0:
            # DB에 값 넣기
            query = "insert into HarmonyDataBase.Member(ID, PW, Drum, Keyboard, LaunchPad) values(%s, %s, %s, %s, %s);"
            curs.execute(query, (id, pw, drum, keyboard, launchpad,))
            conn.commit()
        else:
            return render(request, 'Harmony/signup_error.html')

        return redirect('home')

postnum = 1
def download(request, path):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    if request.method == "GET":
        pk = request.get_full_path()
        postnum = pk[20:]

        # datafetch = "Select p.filepath, p.filename From HarmonyDataBase.Post p where p.postnum = %s;"
        # curs.execute(datafetch, postnum)
        # file_path = curs.fetchall()
        return HttpResponse(pk)
        # audio_file = open(filpath).read()
        #     with open(file_path, 'rb') as fh:
        #         response = HttpResponse(audio_file, content_type="audio/wav")
        #         response['Content-Disposition'] = 'attachment; filename=' + filename
        #         return response


@csrf_exempt
def showfile2(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    datafetch = "Select p.postnum, p.ID, p.title, p.created_date From HarmonyDataBase.Post p;"
    curs.execute(datafetch)
    boardList = curs.fetchall()
    postnum = 1

    if len(boardList) == 0:
        postnum = 1
    else:
        postnum +=1
    if request.method == "POST":
        form = FileForm(request.POST, request.FILES)
        if form.is_valid():
            form.save()
        title = request.POST['subject']
        date = datetime.now()
        contents = request.POST['memo']
        filename = request.FILES['file'].name
        userID = "hufs"
        filepath = "media/files" + filename

        query1 = "insert into HarmonyDataBase.Post(title, filename, created_date, contents, ID, filepath, postnum) values(%s, %s, %s, %s, %s, %s, %s);"
        curs.execute(query1, (title, filename, date, contents, userID, filepath, postnum,))
        conn.commit()

        return redirect('home')
    if request.method == "GET":
        form = FileForm(request.GET, request.FILES)
        curs.execute(datafetch)
        boardList = curs.fetchall()
        return render(request, 'Harmony/writepost.html', {'boardList': boardList, })


def showfile(request, filepath=None):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    if request.method == "GET":
        datafetch = "Select p.postnum, p.ID, p.title, p.created_date From HarmonyDataBase.Post p;"
        curs.execute(datafetch)
        boardList = curs.fetchall()
        if filepath != None:
            return render(request, 'Harmony/listpost.html', {'boardList':boardList, 'filepath':filepath,})
        else:
            return render(request, 'Harmony/listpost.html', {'boardList': boardList,})

def viewWork(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    if request.method == "GET":
        pk = request.get_full_path()
        postnum = pk[20:]

        datafetch = "Select p.postnum, p.ID, p.title, p.contents, p.created_date, p.filepath, p.filename From HarmonyDataBase.Post p;"
        curs.execute(datafetch)
        boardList = curs.fetchall()
        files = File.objects.all()

        for boardRow in boardList:
           if boardRow['postnum'] == postnum:
               return render(request, 'Harmony/readpost.html',
                             {'postnum': boardRow['postnum'], 'ID': boardRow['ID'], 'title': boardRow['title'],
                              'contents': boardRow['contents'], 'created_date': boardRow['created_date'],
                              'filepath': boardRow['filepath'], 'filename': boardRow['filename'], 'data':files,})


def postmodify(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    if request.method == "GET":
        form = FileForm(request.POST, request.FILES)
        return render(request, 'Harmony/postupdate.html', {'form':form,})

    if request.method == "POST":
        pk = request.get_full_path()
        postnum = pk[21:]

        datafetch = "Select p.postnum, p.ID, p.title, p.contents, p.created_date, p.filepath, p.filename From HarmonyDataBase.Post p;"
        curs.execute(datafetch)
        boardList = curs.fetchall()

        for boardRow in boardList:
            if boardRow['postnum'] == postnum:
                form = FileForm(request.POST, request.FILES)
                if form.is_valid():
                    form.save()
                title = request.POST['title']
                date = datetime.now()
                contents = request.POST['contents']
                filename = request.FILES['file'].name
                filepath = "media/files" + filename

                updatedata = 'Update HarmonyDataBase.Post p Set p.title=%s, p.contents=%s, p.filename=%s, p.filepath=%s, p.created_date=%s Where p.postnum=%s;'
                curs.execute(updatedata, (title, contents, filename, filepath, date, postnum,))
                conn.commit()

                return redirect('home')

def postdelete(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    if request.method == "GET":
        pk = request.GET['postnum']
        q1 = " SET SQL_SAFE_UPDATES = 0;"
        curs.execute(q1)
        conn.commit()
        deletedata = """
                   Delete from HarmonyDataBase.Post where Post.postnum = %s;
                    """
        curs.execute(deletedata, pk)
        conn.commit()

        safemode="SET SQL_SAFE_UPDATES = 1;"
        curs.execute(safemode)
        conn.commit()
        return redirect('home')


@csrf_exempt
def index(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    if request.method == "POST":
        message = request.read()
        message = message.decode()

        id_pw_pair = message.split("=")
        id , pw = id_pw_pair[0], id_pw_pair[1]

        query = """
                    SELECT m.ID
                    FROM Member m
                    WHERE m.ID = '""" + id + "'"

        curs.execute(query)
        row = curs.fetchall()

        if len(row) == 0:
            return HttpResponse('Fail,Check again your ID')

        else:
            query2 = "select * from Member where ID = %s"
            curs.execute(query2, id)
            row = curs.fetchall()

            if row[0]['PW'] == pw:
                return HttpResponse("OK")
            else:
                return HttpResponse("Try to check your password")
    elif request.method == "GET":
        return render(request, 'Harmony/index.html')
    conn.close()