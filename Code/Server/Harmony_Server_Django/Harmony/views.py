from datetime import datetime
from .models import File
from .forms import FileForm, LoginForm, CreateUserForm
from django.shortcuts import render, redirect
from django.contrib.auth.models import User
from django.contrib.auth import login, authenticate, logout
from django.template import RequestContext
from django.core.files.storage import FileSystemStorage
from django.http import HttpResponse, request
from django.shortcuts import render, redirect
import pymysql
import sqlite3
from django.views.decorators.csrf import csrf_exempt
from django.utils import timezone


postnum = 1
# 회원가입 버튼 눌릴시 signup.html을 response (GET method), url : /accounts/signup
def signup_pg(request):
    if request.method == "GET":
        return render(request, 'Harmony/signup.html')
    return render(request, 'Harmony/signup/html')


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
        user = User.objects.create_user(
            username=request.POST['ID'],
            password=request.POST['PW']
        )
        user.save()

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

        request.method="GET"
        return showfile(request)

def signout(request):
    logout(request)
    return render(request, 'Harmony/logged_out.html')

def signin(request):
    if request.method == "POST":
        # form = LoginForm(request.POST)
        username = request.POST['username']
        password = request.POST['password']
        user = authenticate(username=username, password=password)
        if user is not None:
            login(request, user, backend='django.contrib.auth.backends.ModelBackend')
            request.method = "GET"
            return showfile(request)
        else:
            # request.session['ID'] = request.POST['username']
            return render(request, 'Harmony/login.html')
    else:
        form = LoginForm()
        return render(request, 'Harmony/login.html', {'form': form})

@csrf_exempt
def showfile2(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    datafetch = "Select p.postnum, p.ID, p.title, p.created_date From HarmonyDataBase.Posts p;"


    if request.method == "POST":
        curs.execute(datafetch)
        boardList = curs.fetchall()
        pn_list = []
        for i in range(len(boardList)):
            pn_list.append(boardList[i]['postnum'])
        global postnum


        if len(boardList) == 0:
            postnum = 1
        else:
            postnum = int(pn_list[-1]) + 1
        title = request.POST['title']
        date = str(datetime.now())[:9]
        contents = request.POST['contents']
        filename = request.FILES['file'].name
        userID = request.user.username
        filepath = "files/" + filename

        myfile = request.FILES['file']
        fs = FileSystemStorage(location='media/files/')
        filename = fs.save(myfile.name, myfile)
        file_url = fs.url(filename)
        form = FileForm(request.FILES, request.FILES)
        if form.is_valid():
            form.save()

        query1 = "insert into HarmonyDataBase.Posts(title, filename, created_date, contents, ID, filepath, postnum) values(%s, %s, %s, %s, %s, %s, %s);"
        if userID == '':
            curs.execute(query1, (title, filename, date, contents, 'Anonymous user', filepath, str(postnum),))
        else:
            curs.execute(query1, (title, filename, date, contents, userID, filepath, str(postnum),))
        postnum += 1
        conn.commit()


        request.method = "GET"
        return showfile(request, file_url)
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
        datafetch = "Select p.postnum, p.ID, p.title, p.created_date From HarmonyDataBase.Posts p;"
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

        datafetch = "Select p.postnum, p.ID, p.title, p.contents, p.created_date, p.filepath, p.filename From HarmonyDataBase.Posts p;"
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

        datafetch = "Select p.postnum, p.ID, p.title, p.contents, p.created_date, p.filepath, p.filename From HarmonyDataBase.Posts p;"
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

                updatedata = 'Update HarmonyDataBase.Posts p Set p.title=%s, p.contents=%s, p.filename=%s, p.filepath=%s, p.created_date=%s Where p.postnum=%s;'
                curs.execute(updatedata, (title, contents, filename, filepath, date, postnum,))
                conn.commit()

                request.method="GET"
                return showfile(request)

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
                   Delete from HarmonyDataBase.Posts where Post.postnum = %s;
                    """
        curs.execute(deletedata, pk)
        conn.commit()

        safemode="SET SQL_SAFE_UPDATES = 1;"
        curs.execute(safemode)
        conn.commit()

        request.method = "GET"
        return showfile(request)


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