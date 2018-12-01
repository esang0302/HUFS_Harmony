from django.shortcuts import render
from django.views.generic.base import TemplateView

class IndexView(TemplateView): # TemplateView를 상속 받는다.
    template_name = 'Harmony/index.html'
# Create your views here.
