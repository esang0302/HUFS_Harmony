"""myProject URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/2.0/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.conf.urls import url, include
from django.contrib import admin
from django.conf import settings
from django.conf.urls.static import static
from Harmony import views

urlpatterns = [
    url(r'^admin/', admin.site.urls),
    url(r'^signup/$', views.signup_pg, name='signup'),
    url(r'^signup/done/$', views.signup, name='signup_done'),
    url(r'^$', views.showfile, name='home'),
    url(r'^viewWork/', views.viewWork, name='read_post'),
    url(r'^viewWork/download/media/files/', views.download),
    url(r'^postmodify/$', views.postmodify),
    url(r'^postdelete/$', views.postdelete),
    #url(r'^stored/$', views.stored),
    url(r'^write/new/$', views.showfile2),
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)