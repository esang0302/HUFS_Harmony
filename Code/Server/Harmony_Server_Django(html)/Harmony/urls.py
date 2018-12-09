from django.conf.urls import url
from . import views

app_name = 'Harmony'

urlpatterns = [
    url(r'^$', views.showfile, name='listpost'),
    url(r'^write/new/$', views.showfile2, name='writepost'),
    url(r'^viewWork/', views.viewWork, name='read_post'),
    url(r'^postmodify/$', views.postmodify),
    url(r'^postdelete/$', views.postdelete),
    url(r'^signup/$', views.signup_pg, name='signup'),
    url(r'^signup/done/$', views.signup, name='signup_done'),
    # /Harmony 으로 접속시 (include 됨)
    # CBV의 generic view를 이용하여 url을 처리하겠다는 말
]