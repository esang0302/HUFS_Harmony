from django.db import models
from django.contrib import admin
from django.contrib.auth.models import User
from django.db import models

class File(models.Model):
    filename= models.CharField(max_length=500)
    filepath= models.FileField(upload_to='files/', null=True, verbose_name="")

    def __str__(self):
        return self.filename + ": " + str(self.filepath)

class Post(models.Model):
    title = models.CharField(max_length=1024)
    content = models.TextField()
    author = models.ForeignKey(User)
    #regdate = models.DataTimeField(auto_created=True, auto_now_add=True)
    def __str__(self):
        return self.title

class PostAdmin(admin.ModelAdmin):
    list_display = ('id', 'title')


class Comment(models.Model):
    post = models.ForeignKey(Post)
    author = models.CharField(max_length=10)
    message = models.TextField()

class DjangoBoard(models.Model):
    title = models.CharField(max_length=50, blank=True)
    filename = models.CharField(max_length=50, blank=True)
    created_date = models.DateField(null=True, blank=True)
    contents = models.CharField(max_length=200, blank=True)
    userID = models.CharField(max_length=500)
    filepath = models.FileField(upload_to='files/', null=True, verbose_name="")
    postnum = models.IntegerField(null=True, blank=True)

    # def __str__(self):
    #     return self.name + ": " + str(self.filepath)