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
    author = models.ForeignKey(User, on_delete=models.PROTECT)
    #regdate = models.DataTimeField(auto_created=True, auto_now_add=True)
    def __str__(self):
        return self.title

class PostAdmin(admin.ModelAdmin):
    list_display = ('id', 'title')


class Comment(models.Model):
    post = models.ForeignKey(Post, on_delete=models.PROTECT)
    author = models.CharField(max_length=10)
    message = models.TextField()
