from django import forms
from polls.models import File
from django import forms
from .models import DjangoBoard

class FileForm(forms.ModelForm):
    class Meta:
        model= File
        fields= ["filename", "filepath"]

class CommentForm(forms.ModelForm):
    class Meta:
        model = DjangoBoard
        fields = ['title', 'filename', 'created_date', 'contents', 'userID', 'filepath', 'postnum']
