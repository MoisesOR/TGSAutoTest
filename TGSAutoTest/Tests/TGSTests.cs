using AventStack.ExtentReports;
using NUnit.Framework;
using System;
using TGSAutoTest.Entities;
using TGSAutoTest.Utils;
using static TGSAutoTest.WebPages.HomePage;
using static TGSAutoTest.WebPages.LeftMenuPage;

namespace TGSAutoTest.Tests
{
    [TestFixture]
    [Category("LoginTests")]
    class LoginTest : TestSetCleanBase
    {
        [TestCase("UserOK")]
        [TestCase("UserFullKO")]
        [TestCase("UserNoName")]
        [TestCase("UserNoPasswd")]
        [TestCase("UserNothing")]
        public void Login(string userType)
        {
            var userName = objectsTests.UserType(userType).UserName;
            var userPassword = objectsTests.UserType(userType).Password;
            homePage.SignIn(userName, userPassword);
            //test.Log(Status.Info, "El usuario hace log in: ", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "El usuario hace log in: ", true);
            homePage.ContinueSignIn();
            var isError = homePage.CheckErrorLogin();

            testHelpers.IsTrueOrFalse(isError, "Login OK", "Validacion OK");
        }
    }

    [TestFixture]
    [Category("E2E Test Split")]
    class E2E : TestSetCleanBase
    {
        [TestCase("NewUser")]
        public void E2E_AlbumFullCreate(string userType)
        {
            var userName = objectsTests.UserType(userType).UserName;
            var userPassword = objectsTests.UserType(userType).Password;
            Album album = new Album(Helpers.GenerateName(), Helpers.GenerateArtist(), Helpers.GetRandomYear(), Helpers.GetRandomString(7), Helpers.GetRandomString(7));

            //Borramos el mismo usuario si esta en la base de datos
            var firstCheckUser = userConnection.CheckUser(userName);
            var checkIfUserDeleted = userConnection.CheckIfUserDeleted(userName);
            var checkUser = userConnection.CheckUser(userName);

            Console.WriteLine("El usuario existe: {0}", firstCheckUser);
            Console.WriteLine("El usuario se ha borrado: {0}", checkIfUserDeleted);
            Console.WriteLine("El usuario existe: {0}", checkUser);

            //test.Log(Status.Pass, "El usuario existe: " + firstCheckUser);
            Logger(Status.Pass, "El usuario existe: " + firstCheckUser, false);
            //test.Log(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted);
            Logger(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted, false);
            //test.Log(Status.Info, "El usuario existe: " + checkUser);
            Logger(Status.Info, "El usuario existe: " + checkUser, false);

            //Creacion el usuario
            homePage.ChangeFront(FrontType.CA);
            homePage.RegisterNewUser(userName, userPassword);
            //test.Log(Status.Info, "Registro del usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Registro del usuario.", true);
            homePage.ContinueRegister();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha registrado correctamente.");
            Assert.True(userConnection.CheckUser(userName), "El usuario no se ha creado correctamente en la base de datos.");


            homePage.SignIn(userName, userPassword);
            //test.Log(Status.Info, "Log in con el nuevo usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Log in con el nuevo usuario.", true);
            homePage.ContinueSignIn();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha logeado correctamente.");
            Assert.IsTrue(page.GetCurrentUrl().Contains("albumList"), "Redireccion incorrecta.");
            Assert.IsTrue(topMenuPage.CheckTopUser(userName), "La barra superior no muestra el usuario correcto del que se ha logeado.");

            leftMenuPage.GoToSelector(MenusType.Album, SubSectionMenu.Create);
            Assert.IsTrue(page.GetCurrentUrl().Contains("createAlbum"), "Redireccion incorrecta.");

            createAlbumPage.CrearAlbum(album);
            //test.Log(Status.Info, "Creacion del album.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Creacion del album.", true);
            createAlbumPage.Continue();
            Assert.False(createAlbumPage.CheckErrorDiv(), "Error al crear el album.");
            Assert.True(albumConection.CheckAlbumName(album), "El album no se ha creado correctamente en la base de datos.");

            Assert.Pass("Se a creado correctamente el usuario y el album en la BBDD.");
        }

        [TestCase("NewUser")]
        public void E2E_AlbumFullDelete(string userType)
        {
            var userName = objectsTests.UserType(userType).UserName;
            var userPassword = objectsTests.UserType(userType).Password;
            Album album = new Album(Helpers.GenerateName(), Helpers.GenerateArtist(), Helpers.GetRandomYear(), Helpers.GetRandomString(7), Helpers.GetRandomString(7));

            //Borramos el mismo usuario si esta en la base de datos
            var firstCheckUser = userConnection.CheckUser(userName);
            var checkIfUserDeleted = userConnection.CheckIfUserDeleted(userName);
            var checkUser = userConnection.CheckUser(userName);

            Console.WriteLine("El usuario existe: {0}", firstCheckUser);
            Console.WriteLine("El usuario se ha borrado: {0}", checkIfUserDeleted);
            Console.WriteLine("El usuario existe: {0}", checkUser);

            //test.Log(Status.Info, "El usuario existe: " + firstCheckUser);
            Logger(Status.Info, "El usuario existe: " + firstCheckUser, false);
            //test.Log(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted);
            Logger(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted, false);
            //test.Log(Status.Info, "El usuario existe: " + checkUser);
            Logger(Status.Info, "El usuario existe: " + checkUser, false);

            //Creacion el usuario
            homePage.ChangeFront(FrontType.CA);
            homePage.RegisterNewUser(userName, userPassword);
            //test.Log(Status.Info, "Creacion del usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Creacion del usuario.", true);
            homePage.ContinueRegister();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha registrado correctamente.");
            Assert.True(userConnection.CheckUser(userName), "El usuario no se ha creado correctamente en la base de datos.");

            homePage.SignIn(userName, userPassword);
            //test.Log(Status.Info, "Sign In del nuevo usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Sign In del nuevo usuario.", true);
            homePage.ContinueSignIn();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha logeado correctamente.");
            Assert.IsTrue(page.GetCurrentUrl().Contains("albumList"), "Redireccion incorrecta.");
            Assert.IsTrue(topMenuPage.CheckTopUser(userName), "La barra superior no muestra el usuario correcto del que se ha logeado.");

            leftMenuPage.GoToSelector(MenusType.Album, SubSectionMenu.Create);
            Assert.IsTrue(page.GetCurrentUrl().Contains("createAlbum"), "Redireccion incorrecta.");

            createAlbumPage.CrearAlbum(album);
            //test.Log(Status.Info, "Album creado correctamente.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Album creado correctamente.", true);
            createAlbumPage.Continue();
            Assert.False(createAlbumPage.CheckErrorDiv(), "Error al crear el album.");
            Assert.True(albumConection.CheckAlbumName(album), "El album no se ha creado correctamente en la base de datos.");

            leftMenuPage.GoToSelector(MenusType.Album, SubSectionMenu.List);
            Assert.IsTrue(page.GetCurrentUrl().Contains("albumList"), "Redireccion incorrecta.");

            var albumId = albumConection.GetAlbumId(album);
            albumListPage.DeleteAlbum(albumId);
            //test.Log(Status.Info, "Album creado correctamente.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Album creado correctamente.", true);
            Assert.False(albumConection.CheckAlbumName(album), "El album no se ha eliminado de la BBDD.");
            Assert.False(albumListPage.IsDeleted(albumId), "El album no se ha eliminado correctamente.");

            Assert.Pass("El album se ha creado y borrado correctamente.");
        }

        [TestCase("NewUser")]
        public void E2E_AlbumFullUpdate(string userType)
        {
            var userName = objectsTests.UserType(userType).UserName;
            var userPassword = objectsTests.UserType(userType).Password;
            Album album = new Album(Helpers.GenerateName(), Helpers.GenerateArtist(), Helpers.GetRandomYear(), Helpers.GetRandomString(7), Helpers.GetRandomString(7));
            Console.WriteLine("Nombre del album: {0}", album.Name);

            //Borramos el mismo usuario si esta en la base de datos
            var firstCheckUser = userConnection.CheckUser(userName);
            var checkIfUserDeleted = userConnection.CheckIfUserDeleted(userName);
            var checkUser = userConnection.CheckUser(userName);

            Console.WriteLine("El usuario existe: {0}", firstCheckUser);
            Console.WriteLine("El usuario se ha borrado: {0}", checkIfUserDeleted);
            Console.WriteLine("El usuario existe: {0}", checkUser);

            //test.Log(Status.Info, "El usuario existe: " + firstCheckUser);
            Logger(Status.Info, "El usuario existe: " + firstCheckUser, false);
            //test.Log(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted);
            Logger(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted, false);
            //test.Log(Status.Info, "El usuario existe: " + checkUser);
            Logger(Status.Info, "El usuario existe: " + checkUser, false);

            //Creacion el usuario
            homePage.ChangeFront(FrontType.CA);
            homePage.RegisterNewUser(userName, userPassword);
            //test.Log(Status.Info, "Creacion del usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Creacion del usuario.", true);
            homePage.ContinueRegister();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha registrado correctamente.");
            Assert.True(userConnection.CheckUser(userName), "El usuario no se ha creado correctamente en la base de datos.");

            homePage.SignIn(userName, userPassword);
            //test.Log(Status.Info, "Sign In del nuevo usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Sign In del nuevo usuario.", true);
            homePage.ContinueSignIn();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha logeado correctamente.");
            Assert.IsTrue(page.GetCurrentUrl().Contains("albumList"), "Redireccion incorrecta.");
            Assert.IsTrue(topMenuPage.CheckTopUser(userName), "La barra superior no muestra el usuario correcto del que se ha logeado.");

            leftMenuPage.GoToSelector(MenusType.Album, SubSectionMenu.Create);
            Assert.IsTrue(page.GetCurrentUrl().Contains("createAlbum"), "Redireccion incorrecta.");

            createAlbumPage.CrearAlbum(album);
            //test.Log(Status.Info, "Creacion del album.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Creacion del album.", true);
            createAlbumPage.Continue();
            Assert.False(createAlbumPage.CheckErrorDiv(), "Error al crear el album.");
            Assert.True(albumConection.CheckAlbumName(album), "El album no se ha creado correctamente en la base de datos.");

            leftMenuPage.GoToSelector(MenusType.Album, SubSectionMenu.Update);
            Assert.IsTrue(page.GetCurrentUrl().Contains("updateAlbum"), "Redireccion incorrecta.");

            Album updateAlbum = new Album(Helpers.GenerateName(), Helpers.GenerateArtist(), Helpers.GetRandomYear(), Helpers.GetRandomString(7), Helpers.GetRandomString(7));
            updateAlbumPage.SelectId(albumConection.GetAlbumId(album));
            updateAlbumPage.UpdateAlbum(updateAlbum);
            //test.Log(Status.Info, "Actualizacion del album.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Actualizacion del album.", true);
            updateAlbumPage.Continue();

            Assert.True(albumConection.CheckAlbumName(updateAlbum), "El album no se ha actualizado correctamente en la base de datos.");
            Assert.IsFalse(updateAlbumPage.IsError(), "Redireccion incorrecta.");

            Assert.Pass("El album se ha actualizado correctamente.");
        }
    }

    [TestFixture]
    [Category("Full E2E Test")]
    class FullE2E : TestSetCleanBase
    {
        [TestCase("NewUser")]
        public void FullE2E_GroupCreateUpdateDelete(string userType)
        {
            var userName = objectsTests.UserType(userType).UserName;
            var userPassword = objectsTests.UserType(userType).Password;
            Group group = new Group(Helpers.GenerateName(), Helpers.GetRandomNumberBetween(0, 1790), Helpers.GetRandomNumberBetween(1790, DateTime.Today.Year), Helpers.GetRandomString(7), Helpers.GetRandomString(7), Helpers.GetRandomString(7), Helpers.GetRandomString(20));
            Console.WriteLine("Nombre del group: {0}", group.Name);

            //Borramos el mismo usuario si esta en la base de datos
            var firstCheckUser = userConnection.CheckUser(userName);
            var checkIfUserDeleted = userConnection.CheckIfUserDeleted(userName);
            var checkUser = userConnection.CheckUser(userName);

            Console.WriteLine("El usuario existe: {0}", firstCheckUser);
            Console.WriteLine("El usuario se ha borrado: {0}", checkIfUserDeleted);
            Console.WriteLine("El usuario existe: {0}", checkUser);

            //test.Log(Status.Info, "El usuario existe: " + firstCheckUser);
            Logger(Status.Info, "El usuario existe: " + firstCheckUser, false);
            //test.Log(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted);
            Logger(Status.Info, "El usuario se ha borrado: " + checkIfUserDeleted, false);
            //test.Log(Status.Info, "El usuario existe: " + checkUser);
            Logger(Status.Info, "El usuario existe: " + checkUser, false);

            //Creacion el usuario
            homePage.ChangeFront(FrontType.CA);
            homePage.RegisterNewUser(userName, userPassword);
            //test.Log(Status.Info, "Creacion del usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Creacion del usuario.", true);
            homePage.ContinueRegister();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha registrado correctamente.");
            Assert.True(userConnection.CheckUser(userName), "El usuario no se ha creado correctamente en la base de datos.");

            homePage.SignIn(userName, userPassword);
            //test.Log(Status.Info, "Sign In del nuevo usuario.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Sign In del nuevo usuario.", true);
            homePage.ContinueSignIn();
            Assert.False(homePage.CheckErrorLogin(), "Error, el usuario no se ha logeado correctamente.");
            Assert.IsTrue(page.GetCurrentUrl().Contains("albumList"), "Redireccion incorrecta.");
            Assert.IsTrue(topMenuPage.CheckTopUser(userName), "La barra superior no muestra el usuario correcto del que se ha logeado.");

            leftMenuPage.GoToSelector(MenusType.Group, SubSectionMenu.Create);
            Assert.IsTrue(page.GetCurrentUrl().Contains("createGroup"), "Redireccion incorrecta.");

            createGroupPage.CrearGroup(group);
            //test.Log(Status.Info, "Creacion del grupo.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Creacion del grupo.", true);
            createGroupPage.Continue();
            Assert.False(createGroupPage.CheckErrorDiv(), "Error al crear el grupo.");
            Assert.True(groupConnection.CheckGroupName(group), "El album no se ha creado correctamente en la base de datos.");

            leftMenuPage.GoToSelector(MenusType.Group, SubSectionMenu.Update);
            Assert.IsTrue(page.GetCurrentUrl().Contains("updateGroup"), "Redireccion incorrecta.");

            Group updateGroup = new Group(Helpers.GenerateName(), Helpers.GetRandomNumberBetween(0, 1790), Helpers.GetRandomNumberBetween(1790, DateTime.Today.Year), Helpers.GetRandomString(7), Helpers.GetRandomString(7), Helpers.GetRandomString(7), Helpers.GetRandomString(20));
            updateGroupPage.SelectId(groupConnection.GetGroupId(group));
            updateGroupPage.UpdateGroup(updateGroup);
            //test.Log(Status.Info, "Actualizacion del grupo.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Actualizacion del grupo.", true);
            updateGroupPage.Continue();

            Assert.IsFalse(updateGroupPage.IsError(), "Error al actualizar el grupo.");
            Assert.True(groupConnection.CheckGroupName(updateGroup), "El grupo no se ha actualizado correctamente en la base de datos.");

            leftMenuPage.GoToSelector(MenusType.Group, SubSectionMenu.List);
            Assert.IsTrue(page.GetCurrentUrl().Contains("groupList"), "Redireccion incorrecta.");

            //Eliminar el grupo actualizado
            var groupId = groupConnection.GetGroupId(updateGroup);
            groupListPage.DeleteGroup(groupId);
            //test.Log(Status.Info, "Album borrado correctamente.", MediaEntityBuilder.CreateScreenCaptureFromPath(page.TakeScreenshot(SetUpFixtureBase.HTMLPath)).Build());
            Logger(Status.Info, "Album borrado correctamente.", true);
            Assert.False(groupConnection.CheckGroupName(updateGroup), "El grupo no se ha eliminado de la BBDD.");
            Assert.False(groupListPage.IsDeleted(groupId), "El grupo no se ha eliminado correctamente.");

            Assert.Pass("El grupo se ha creado, actualizado y borrado correctamente.");
        }
    }
}