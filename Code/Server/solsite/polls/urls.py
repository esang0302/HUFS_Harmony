from django.conf.urls import url
from . import views

app_name = 'polls'

urlpatterns = [
    url(r'^$', views.showfile),
    # /Harmony 으로 접속시 (include 됨)
    # CBV의 generic view를 이용하여 url을 처리하겠다는 말
]