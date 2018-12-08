from datetime import timezone
from .models import File
from .forms import FileForm
import os
from django.conf import settings
from django.http import HttpResponse, request
from django.shortcuts import render, redirect
import pymysql
import sqlite3
from django.views.decorators.csrf import csrf_exempt
from django.utils import timezone

postnum = 1
def download(request, path):
    file_path = os.path.join(settings.MEDIA_ROOT, path)
    if os.path.exists(file_path):
        with open(file_path, 'rb') as fh:
            response = HttpResponse(fh.read(), content_type="application/vnd.ms-excel")
            response['Content-Disposition'] = 'inline; filename=' + os.path.basename(file_path)
            return response

@csrf_exempt
def stored(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    if request.method == "POST":
        lastfile = File.objects.last()
        #lastfile = File()

        filepath = lastfile.filepath
        #title = lastfile.title
        #date = lastfile.created_date
        #contents = lastfile.contents
        filename = lastfile.filename
        #userID=lastfile.userID
        #postnum = lastfile.postnum

        conn2 = sqlite3.connect("db.sqlite3")
        cur = conn2.cursor()
        cur.execute("select * from polls_file")
        rows = cur.fetchall()
        return HttpResponse(rows)
        filepath2 = rows[-1][2]
        form = FileForm(request.POST, request.FILES)
        if form.is_valid():
            form.save()

            context = {'filepath': filepath,
                       'form': form,
                       'filename': filename
                       }
            query = "insert into HarmonyDataBase.Post(filename, path) values(%s, %s);"
            curs.execute(query, (filename, filepath2,))
            conn.commit()
        return render(request, 'polls/writepost.html', {'form': form, })
    if request.method == "GET":
        form = FileForm(request.GET, request.FILES)
        return render(request, 'polls/writepost.html', {'form': form, })

@csrf_exempt
def showfile2(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)

    if request.method == "POST":
        global postnum
        #lastfile = File.objects.last()
        # lastfile = DjangoBoard()
        #
        #filepath = lastfile.filepath
        #         title = lastfile.title
        #         date = timezone.now()
        #         contents = lastfile.contents
        #         filename = lastfile.filename
        #         userID=lastfile.userID
        #         postnum = lastfile.postnum

        # filepath = request.POST['filepath']
        # title = request.POST['title']
        # date = timezone.now()
        # contents = request.POST['contents']
        # filename = request.POST['filename']
        # userID=request.POST['userID']
        # postnum = request.POST['postnum']

        form = FileForm(request.POST, request.FILES)
        if form.is_valid():
            form.save()
        conn2 = sqlite3.connect("db.sqlite3")
        cur = conn2.cursor()
        cur.execute("select * from polls_file")
        rows = cur.fetchall()
        filepath = rows[-1][2]
        #postnum = rows[-1][0]
            # context = {'filepath': filepath,
            #            'form': form,
            #            'filename': filename
            #            }
        query = "insert into HarmonyDataBase.Post(title, filename, created_date, contents, ID, filepath, postnum) values(%s, %s, %s, %s, %s, %s, %s);"
        curs.execute(query, (request.POST['title'], request.POST['filename'], str(timezone.now())[:-10], request.POST['contents'], request.POST['userID'], filepath, postnum,))

        postnum += 1
        conn.commit()

        request.method = "GET"
        return showfile(request, filepath)
    if request.method == "GET":
        form = FileForm(request.POST, request.FILES)
        return render(request, 'polls/writepost.html', {'form':form,})


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
            return render(request, 'polls/listpost.html', {'boardList':boardList, 'filepath':filepath,})
        else:
            return render(request, 'polls/listpost.html', {'boardList': boardList,})

def viewWork(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    if request.method == "GET":
        pk = request.GET['postnum']
        datafetch = "Select p.postnum, p.ID, p.title, p.contents, p.created_date, p.filepath, p.filename From HarmonyDataBase.Post p;"
        curs.execute(datafetch)
        boardList = curs.fetchall()

        for boardRow in boardList:
            if boardRow['postnum'] == pk:
                return render(request, 'polls/readpost.html', {'postnum':boardRow['postnum'], 'ID':boardRow['ID'], 'title':boardRow['title'], 'contents':boardRow['contents'], 'created_date':boardRow['created_date'], 'filepath':boardRow['filepath'], 'filename':boardRow['filename'],})

def postmodify(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    pk = request.GET['postnum']
    if request.method == "GET":
        form = FileForm(request.POST, request.FILES)
        return render(request, 'polls/postupdate.html', {'form':form,})

    if request.method == "POST":
        datafetch = "Select p.postnum, p.ID, p.title, p.contents, p.created_date, p.filepath, p.filename From HarmonyDataBase.Post p;"
        curs.execute(datafetch)
        boardList = curs.fetchall()

        for boardRow in boardList:
            if boardRow['postnum'] == pk:
                form = FileForm(request.POST, request.FILES)
                if form.is_valid():
                    form.save()
                conn2 = sqlite3.connect("db.sqlite3")
                cur = conn2.cursor()
                cur.execute("select * from polls_file")
                rows = cur.fetchall()
                filepath = rows[-1][2]
                ID = request.POST['userID']
                title = request.POST['title']
                contents = request.POST['contents']
                filename = request.POST['filename']

                updatedata = 'Update HarmonyDataBase.Post p Set p.ID=ID, p.title=title, p.contents=contents, p.filename=filename, p.filepath=filepath Where p.postnum=@pk;'
                curs.execute(updatedata)

                request.method = "GET"
                return viewWork(request)

def postdelete(request):
    conn = pymysql.connect(host='aa1ob3qqoho0ocv.c8ijhazzdpdc.ap-northeast-2.rds.amazonaws.com',
                           user='KimSeyong',
                           password='npproject',
                           db='HarmonyDataBase',
                           charset='utf8')
    curs = conn.cursor(pymysql.cursors.DictCursor)
    if request.method == "GET":
        pk = request.GET['postnum']
        deletedata = 'Delete from HarmonyDataBase.Post where postnum = @pk;'
        curs.execute(deletedata)

        return render(request, 'polls/listpost.html')
